using Obi;
using UnityEngine;

public class DeathState : MonoBehaviour, IState<Snake>
{
    private SnakeMovement _movement;
    private ObiRope _rope;

    public void Init(Snake main)
    {
        _movement = main.Movement;
        _rope = main.Rope;
    }

    public void Enter()
    {
        Debug.Log("Dead");
        _movement.SetVelocity(Vector3.forward * _movement.CurrentSpeed);
        _rope.selfCollisions = true;
    }

    public void Exit()
    {

    }

}
