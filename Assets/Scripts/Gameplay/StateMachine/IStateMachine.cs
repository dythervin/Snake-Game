using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine
{
    IState CurrentState { get; }
    void Enter<TState>() where TState : class, IState;
}
