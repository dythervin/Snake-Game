using UnityEngine;

public class FollowZ : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 _position;

    private void Awake()
    {
        _position = transform.position;
    }

    private void Update()
    {
        var targetZ = target.transform.position.z;

        _position.z = targetZ;
        transform.position = _position;
    }
}
