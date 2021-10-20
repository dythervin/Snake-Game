using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeStateMachine : MonoBehaviour, IStateMachine
{
    [SerializeField] private Snake snake;
    [SerializeField] private UnityEvent onDeath;
    private readonly Dictionary<Type, IState<Snake>> _states = new Dictionary<Type, IState<Snake>>();

    private IState _currentState;

    public IState CurrentState => _currentState;
    private bool _alive = true;

    private void Awake()
    {
        IState<Snake>[] states = GetComponents<IState<Snake>>();
        foreach (var state in states)
        {
            _states.Add(state.GetType(), state);
            state.Init(snake);
        }

        var movingState = GetState<MovingControlState>();
        movingState.OnDeath += OnDeath;
        Enter<IdleState>();
    }

    public void OnGameStart()
    {
        Enter<MovingControlState>();
    }

    public void Enter<TState>() where TState : class, IState
    {
        if (!_alive)
            return;

        TState newState = GetState<TState>();
        if (newState == _currentState)
            return;

        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();

        Debug.Log($"{name} entered {typeof(TState)} state");
    }

    public void EnterFeverState()
    {
        Enter<FeverState>();
    }

    private TState GetState<TState>() where TState : class, IState
        => _states[typeof(TState)] as TState;

    private void OnDeath()
    {
        Enter<DeathState>();
        _alive = false;
        onDeath.Invoke();
    }
}
