using UnityEngine;

public class Floating : MonoBehaviour
{
    [SerializeField] protected float magnitude = 1;
    [SerializeField] protected float speedMultiplier = 2;
    [SerializeField] protected float offset;
    private float _initialY;

    protected virtual void Awake()
    {
        _initialY = transform.position.y + magnitude;
    }


    private void Update()
    {
        Vector3 position = transform.position;
        position.y = _initialY + Mathf.Sin(offset + Time.time * speedMultiplier) * magnitude;
        transform.position = position;
    }
}
