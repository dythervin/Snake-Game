using Obi;
using UnityEngine;

public class SnakeColored : Colored
{
    [SerializeField] private ObiRope rope;

    private Color color;
    private ObiColliderWorld world;


    private void OnDestroy()
    {
        if (rope != null)
            rope.solver.OnCollision -= Coloring;

    }

    private void Start()
    {
        world = ObiColliderWorld.GetInstance();
        SetAll(color);
        rope.solver.OnCollision += Coloring;
    }

    private void Coloring(ObiSolver solver, ObiSolver.ObiCollisionEventArgs e)
    {
        foreach (Oni.Contact contact in e.contacts)
        {
            if (contact.distance < 0.01)
            {
                ObiColliderBase col = world.colliderHandles[contact.bodyB].owner;

                if (col.gameObject.CompareTag(Tags.ColoringTrigger))
                {
                    int particleIndex = solver.simplices[contact.bodyA];

                    solver.colors[particleIndex] = color;
                }
            }
        }
    }

    public override int ColorId
    {
        get => base.ColorId;
        set
        {
            base.ColorId = value;
            color = Colors.Instance.GetColor(value);
        }
    }

    private void SetAll(in Color color)
    {
        for (int i = 0; i < rope.particleCount; i++)
        {
            int index = rope.solverIndices[i];
            rope.solver.colors[index] = color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.ColoringTrigger))
        {
            if (other.TryGetComponent(out IColored coloringTrigger))
            {
                ColorId = coloringTrigger.ColorId;
            }
        }
    }
}
