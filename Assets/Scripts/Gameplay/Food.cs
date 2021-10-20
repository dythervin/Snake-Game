using Obi;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour, IFood
{
    [Header(nameof(Food))]
    [SerializeField] private LinearMovement movement;
    [SerializeField] private Collider[] colliders;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private ObiCollider[] obiColliders;
    [SerializeField] private UnityEvent onMonthReached;



    private void OnEnable()
    {
        movement.OnReached += OnMonthReached;
    }

    private void OnDisable()
    {
        movement.OnReached -= OnMonthReached;
    }

    public void MoveToMouth(Transform mouth, in float speed)
    {
        foreach (Collider collider in colliders)
            collider.enabled = false;

        foreach (ObiCollider collider in obiColliders)
            collider.enabled = false;

        if (rigidbody != null)
            rigidbody.isKinematic = true;

        movement.Move(mouth, speed);
    }

    public void MoveToMouth(Transform mouth, in float speed, in Vector3 offset)
    {
        movement.offset = offset;
        MoveToMouth(mouth, speed);
    }

    public void ScaleToZero(in float duration)
    {
        ScaleTo(Vector3.zero, duration);
    }

    public void OnMonthReached()
    {
        onMonthReached.Invoke();
        ScaleTo(Vector3.zero, 0.1f);
    }

    private void ScaleTo(Vector3 target, float duration)
    {
        StartCoroutine(Scale());


        IEnumerator Scale()
        {
            Vector3 start = transform.localScale;
            for (float t = 0; t < 1; t += Time.deltaTime / duration)
            {
                transform.localScale = Vector3.Lerp(start, target, t);
                yield return null;
            }
            if (target == Vector3.zero)
                Destroy(gameObject);
        }
    }
}
