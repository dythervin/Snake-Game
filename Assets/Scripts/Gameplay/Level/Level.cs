using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private float xRange = 10;
    [SerializeField] private Transform snakeSpawnPosition;

    public float XRange => xRange;
    public Vector3 SnakeSpawnPosition => snakeSpawnPosition.position;
}
