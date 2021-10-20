using Obi;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{

    [SerializeField] private float baseSpeed = 5;
    [SerializeField] private float acceleration = 10;
    [SerializeField] private int everyNSubstep = 2;

    [System.NonSerialized]
    public float TargetX;


    private ObiRope _rope;
    private float _currentSpeed;
    private int _headIndex;
    private Coroutine _accelerating;
    private int _substep;

    //private float[] _traction;
    //private Vector3[] _surfaceNormals;

    public float BaseSpeed => baseSpeed;
    public Vector3 HeadPosition => _rope.solver.positions[_headIndex];

    [ShowInInspector, ReadOnly]
    public float CurrentSpeed
    {
        get => _currentSpeed / everyNSubstep;
        set => _currentSpeed = value * everyNSubstep;
    }

    private void Awake()
    {
        _rope = GetComponentInChildren<ObiRope>();
    }

    private void Start()
    {
        _headIndex = _rope.solverIndices[0];
    }


    public void Move(ObiSolver solver, float stepTime)
    {
        if (_substep++ % everyNSubstep != 0)
            return;


        solver.velocities[_headIndex] = new Vector3(
            (TargetX - HeadPosition.x) / stepTime,
            0,
            _currentSpeed);

        float avgDistBtwParticles = _rope.interParticleDistance * 4;
        Vector3 prevPosition = solver.positions[_rope.solverIndices[0]];

        for (int i = 1; i < _rope.activeParticleCount; i++)
        {
            int currIndex = _rope.solverIndices[i];
            Vector3 currPosition = solver.positions[currIndex];


            Vector3 dir = (prevPosition - currPosition);
            float distMultiplier = dir.magnitude / avgDistBtwParticles;

            if (distMultiplier < 1 && distMultiplier > 0)
            {
                dir *= distMultiplier / stepTime;
            }
            else
            {
                dir /= stepTime;
            }


            //Vector3.ProjectOnPlane(prevPosition - currPosition, _surfaceNormals[i]).normalized;

            //dir.y = solver.velocities[currIndex].y * 0.99f;
            solver.velocities[currIndex] = dir;

            prevPosition = currPosition;
        }
    }

    //private float GetSpeedWithTraction(in int ropeIndex)
    //    =>
    //    _traction[ropeIndex] *
    //    Speed;


    public void AccelerateTo(in float targetSpeed)
    {
        if (_accelerating != null)
            StopCoroutine(_accelerating);

        _accelerating = StartCoroutine(Accelerating(targetSpeed));
    }


    public void SetVelocity(in Vector4 dir)
    {
        for (int i = 1; i < _rope.activeParticleCount; i++)
        {
            int currIndex = _rope.solverIndices[i];
            _rope.solver.velocities[currIndex] = dir;
        }
    }

    private IEnumerator Accelerating(float targetSpeed)
    {
        while (_currentSpeed != targetSpeed)
        {
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, acceleration * everyNSubstep * Time.fixedDeltaTime);
            yield return Waiters.FixedUpdate;
        }
    }

}
