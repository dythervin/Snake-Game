using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LinearMovement : MonoBehaviour
{
    [SerializeField] private UnityEvent onStartedMovement;
    public Vector3 offset;

    public event System.Action OnReached;

    private Coroutine _coroutine;
    public void Move(in Vector3 target, in float speed)
    {
        Stop();

        onStartedMovement?.Invoke();
        _coroutine = StartCoroutine(MoveTo(target, speed));
    }


    public void Move(Transform target, in float speed)
    {
        Stop();

        onStartedMovement?.Invoke();
        _coroutine = StartCoroutine(MoveTo(target, speed));
    }

    public void Stop()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }


    private IEnumerator MoveTo(Vector3 target, float speed)
    {
        Vector3 start = transform.position;
        target += offset;
        float time = Vector3.Distance(start, target) / speed;

        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
        OnReached?.Invoke();
    }

    private IEnumerator MoveTo(Transform target, float speed)
    {
        Vector3 start = transform.position;
        float time = Vector3.Distance(start, target.position) / speed;

        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            if (target == null)
                yield break;

            transform.position = Vector3.Lerp(start, offset + target.position, t);
            yield return null;
        }
        OnReached?.Invoke();
    }
}
