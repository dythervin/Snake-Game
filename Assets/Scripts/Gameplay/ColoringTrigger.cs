using UnityEngine;

public class ColoringTrigger : Colored
{
    [SerializeField] private Collider trigger;

    private static readonly int _emmisionColor = Shader.PropertyToID("_EmissionColor");

    protected override void Awake()
    {
        base.Awake();
        if (!trigger.isTrigger)
            trigger.isTrigger = true;
    }

    public override int ColorId
    {
        get => base.ColorId;
        set
        {
            colorId = value;
            Color color = Colors.Instance.GetColor(colorId);

            foreach (Renderer renderer in renderers)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    renderer.sharedMaterial.SetColor(_emmisionColor, color);
                else
#endif
                    renderer.material.SetColor(_emmisionColor, color);
            }
        }
    }
}
