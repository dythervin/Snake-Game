using Obi;
using UnityEngine;

public class IdleState : MonoBehaviour, IState<Snake>
{
    ObiRope _rope;
    void IState<Snake>.Init(Snake main)
    {
        _rope = main.Rope;
    }
    void IState.Enter()
    {
        _rope.selfCollisions = true;
    }

    void IState.Exit()
    {
        _rope.selfCollisions = false;
    }

}
