using Obi;
using Sirenix.OdinInspector;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [Header(nameof(Snake))]
    [SerializeField] private ObiRope obiRope;
    [SerializeField] private RopeCollisionData ropeCollisionData;
    [SerializeField] private Colored colored;
    [SerializeField] private SnakeMovement snakeMovement;
    [SerializeField] private float radius = 1;

    public ObiRope Rope => obiRope;
    public RopeCollisionData CollisionData => ropeCollisionData;
    public SnakeMovement Movement => snakeMovement;
    public Colored Colored => colored;
    public float Radius => radius;


    //private void Start()
    //{
    //ropeCollisionData.Init(obiRope);
    //}

    [Button]
    private void GetAvgDist()
    {
        Debug.Log(Rope.interParticleDistance);
    }
}
