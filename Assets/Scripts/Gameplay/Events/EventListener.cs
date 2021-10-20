using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour, IListener
{
    [SerializeField] private EventAsset[] events;

    [Space]
    [SerializeField] private bool delayed;

    [ShowIf(nameof(delayed))]
    [Tooltip("Allow multiple delayed calls")]
    [SerializeField] private bool allowMultiple;

    [ShowIf(nameof(delayed))]
    [Tooltip("In seconds, waits at least a frame")]
    [SerializeField, Min(0)] private float delay;

    [Space]
    [SerializeField] private UnityEvent onRaise;
    private int _delayedRoutines;

    void IListener.Invoke()
    {
        if (delayed)
        {
            if (allowMultiple || _delayedRoutines == 0)
                StartCoroutine(DelayedRaise());
        }
        else
        {
            OnRaise();
        }
    }

    private IEnumerator DelayedRaise()
    {
        _delayedRoutines++;
        yield return delay == 0 ? null : new WaitForSeconds(delay);
        OnRaise();
        _delayedRoutines--;
    }

    protected virtual void OnRaise()
    {
        onRaise.Invoke();
    }

    private void OnEnable()
    {
        foreach (EventAsset eventAsset in events)
            eventAsset.AddListener(this);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        foreach (EventAsset eventAsset in events)
            eventAsset.RemoveListener(this);
    }
}
