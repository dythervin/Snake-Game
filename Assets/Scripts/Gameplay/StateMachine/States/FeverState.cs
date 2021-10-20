using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public sealed class FeverState : MovingState
{
    [SerializeField] private float duration = 5;
    [SerializeField] private UnityEvent onExit;
    private WaitForSeconds _duration;

    private IStateMachine _stateMachine;

    public override void Init(Snake main)
    {
        base.Init(main);
        _duration = new WaitForSeconds(duration);
        TryGetComponent(out _stateMachine);
    }

    public override void Enter()
    {
        base.Enter();
        StartCoroutine(DelayedEnterMoving());
    }

    public override void Exit()
    {
        onExit.Invoke();
        base.Exit();
    }

    private IEnumerator DelayedEnterMoving()
    {
        yield return _duration;
        _stateMachine.Enter<MovingControlState>();
    }

    protected override void OnCollisionEnter(Collision collision) { }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!enabled)
            return;

        if (other.CompareTag(Tags.Food) || other.CompareTag(Tags.Obstacle))
        {
            Eat(other);
        }
    }

    protected override void Eat(Component other)
    {
        IFood Food = other.GetComponent<IFood>();
        if (Food == null)
            return;

        Food.MoveToMouth(transform, Mathf.Max(Movement.CurrentSpeed, Movement.BaseSpeed * speedMultiplier), new Vector3(0, -transform.position.y, 0));
    }
}
