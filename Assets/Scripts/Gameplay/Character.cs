using Sirenix.OdinInspector;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header(nameof(Character))]
    [SerializeField] private Animator animator;

    [SerializeField] private bool randomIdle;

    [DisableIf(nameof(randomIdle))]
    [SerializeField] private int idleId;

    [DisableIf(nameof(randomIdle))]
    [SerializeField] private bool mirror;

    private readonly int IdleAnimHash = Animator.StringToHash("Idle");
    private readonly int MirrorAnimHash = Animator.StringToHash("Mirror");
    private const int IdleAnimationsLenght = 3;

    private void Awake()
    {
        if (randomIdle)
        {
            idleId = Random.Range(0, IdleAnimationsLenght);
            mirror = Random.value > 0.5f;
        }

        animator.SetFloat(IdleAnimHash, idleId);
        animator.SetBool(MirrorAnimHash, mirror);
    }
}
