using Obi;
using UnityEngine;

public class MovingState : MonoBehaviour, IState<Snake>
{
    [SerializeField, Min(0.1f)] protected float speedMultiplier = 1;
    [SerializeField] private bool accelerate;

    public event System.Action OnDeath;

    protected SnakeMovement Movement;

    private Colored colorComponent;
    private Snake _snake;
    private ObiRope _rope;


    protected float Speed
    {
        get => Movement.CurrentSpeed;
        set => Movement.CurrentSpeed = value;
    }

    public virtual void Init(Snake main)
    {
        enabled = false;
        _snake = main;
        _rope = _snake.Rope;
        Movement = main.Movement;
        colorComponent = main.Colored;
    }


    public virtual void Enter()
    {
        enabled = true;
        _rope.selfCollisions = false;
        Movement.TargetX = 0;

        float targetSpeed = _snake.Movement.BaseSpeed * speedMultiplier;
        if (accelerate)
            Movement.AccelerateTo(targetSpeed);
        else
            Speed = targetSpeed;

        _rope.solver.OnSubstep += Movement.Move;

    }



    public virtual void Exit()
    {
        _rope.solver.OnSubstep -= Movement.Move;
        _rope.selfCollisions = true;
        StopAllCoroutines();
        enabled = false;
    }

    private void OnDestroy()
    {
        if (enabled)
            Exit();
    }




    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (!enabled)
            return;

        if (collision.collider.CompareTag(Tags.Obstacle))
        {
            OnDeath?.Invoke();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!enabled)
            return;

        if (other.CompareTag(Tags.Food))
        {
            Eat(other);
        }
    }

    protected virtual void Eat(Component other)
    {
        IFood Food = other.GetComponent<IFood>();
        if (Food == null)
            return;


        if (other.TryGetComponent(out IColored colored) && colored.ColorId != colorComponent.ColorId)
        {
            OnDeath?.Invoke();
        }
        else
        {
            Food.MoveToMouth(transform, Movement.BaseSpeed * speedMultiplier, new Vector3(0, -transform.position.y, 0));
        }
    }


}
