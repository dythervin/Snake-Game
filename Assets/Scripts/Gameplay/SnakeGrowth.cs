using Obi;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class SnakeGrowth : MonoBehaviour
{
    [SerializeField] private float foodAddLenght = 0.5f;
    [SerializeField] private int particleCount = 10;
    [SerializeField] private float step = 1f;
    [SerializeField] private float maxScale = 1.5f;


    private ObiRope _rope;
    private ObiRopeCursor _ropeCursor;
    private float _targetLength;

    [ShowInInspector, ReadOnly]
    private float _currentLength;

    private Vector3 _baseThickness;
    private Vector3 _maxThickness;
    private int _immutableParticleCount;

    public void Grow()
    {
        StartCoroutine(Growing());
    }

    private void Awake()
    {
        _rope = GetComponentInChildren<ObiRope>();
        _rope.TryGetComponent(out _ropeCursor);
    }

    private void Start()
    {
        _currentLength = _rope.restLength;
        _targetLength = _currentLength;
        _baseThickness = GetScale(0);
        _maxThickness = _baseThickness * maxScale;
        step *= Time.fixedDeltaTime;
        _immutableParticleCount = _rope.activeParticleCount - Mathf.FloorToInt(_ropeCursor.cursorMu * _rope.activeParticleCount);
    }



    private void Update()
    {
        if (_currentLength < _targetLength)
        {
            _currentLength += Time.deltaTime;
            _ropeCursor.ChangeLength(_currentLength);
        }
    }
    private IEnumerator Growing()
    {
        for (int i = 0; i < CursorIndex; i++)
        {
            int growLast = Mathf.Max(i - particleCount, -1);
            Grow(i, growLast);

            int shrinkLast = Mathf.Max(i - particleCount * 2, -1);
            Shrink(growLast, shrinkLast);

            yield return Waiters.FixedUpdate;
        }

        for (int i = CursorIndex - particleCount; i <= CursorIndex + 1; i++)
        {
            Shrink(i, i - particleCount);
            yield return Waiters.FixedUpdate;
        }


        _targetLength += foodAddLenght;


    }

    private int CursorIndex => _rope.activeParticleCount - _immutableParticleCount;

    private void Grow(int i, int to)
    {
        for (; i > to; i--)
        {
            if (i >= _rope.activeParticleCount)
                continue;

            Vector3 curr = GetScale(i);
            curr = Vector3.MoveTowards(curr, _maxThickness, step);
            SetScale(i, curr);
        }
    }

    private void Shrink(int i, int to)
    {
        for (; i > to; i--)
        {
            if (i >= _rope.activeParticleCount)
                continue;

            Vector3 curr = GetScale(i);
            curr = Vector3.MoveTowards(curr, _baseThickness, step * 1.5f);
            SetScale(i, curr);
        }
    }

    private Vector3 GetScale(in int index)
    {
        return _rope.solver.principalRadii[_rope.solverIndices[index]];
    }

    private void SetScale(in int index, in Vector3 value)
    {
        _rope.solver.principalRadii[_rope.solverIndices[index]] = value;
    }
}
