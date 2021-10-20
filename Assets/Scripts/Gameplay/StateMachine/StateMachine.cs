using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
    private IState _currentState;
    public IState CurrentState => _currentState;

    public void Enter<TState>() where TState : class, IState
    {
        _currentState?.Exit();
        _currentState = GetState<TState>();
        _currentState.Enter();
    }

    private TState GetState<TState>() where TState : class, IState
        => _states[typeof(TState)] as TState;
}
