using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/New Event Asset")]
public class EventAsset : ScriptableObject
{
    private readonly HashSet<IListener> _listeners = new HashSet<IListener>();
    private readonly HashSet<IListener> _listenersToAdd = new HashSet<IListener>();
    private readonly HashSet<IListener> _listenersToRemove = new HashSet<IListener>();

    private bool _executing;


    public void Invoke()
    {
        _executing = true;

        foreach (var listener in _listeners)
            listener.Invoke();


        if (_listenersToAdd.Count > 0)
        {
            foreach (var listener in _listenersToRemove)
                _listenersToAdd.Remove(listener);


            foreach (var listener in _listenersToAdd)
                _listeners.Add(listener);

            _listenersToAdd.Clear();
        }

        if (_listenersToRemove.Count > 0)
        {
            foreach (var listener in _listenersToRemove)
                _listeners.Remove(listener);

            _listenersToRemove.Clear();
        }

        _executing = false;
    }

    public void AddListener(in IListener listener)
    {
        if (_listeners.Contains(listener))
            return;


        var container = _executing ? _listenersToAdd : _listeners;

        container.Add(listener);
    }

    public void RemoveListener(in IListener listener)
    {
        if (_executing)
            _listenersToRemove.Add(listener);
        else
        {
            _listeners.Remove(listener);
            _listenersToAdd.Remove(listener);
        }
    }
}
