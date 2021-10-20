using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();

    void Exit();
}

public interface IState<TMain> : IState
{
    public  void Init(TMain main);
}
