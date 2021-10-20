using Sirenix.OdinInspector;
using UnityEngine;

public class Colored : MonoBehaviour, IColored
{
    [Header("Color")]
    [SerializeField] protected Renderer[] renderers;
    [SerializeField] protected bool randomColor;

    [DisableIf(nameof(randomColor))]
#if UNITY_EDITOR
    [ValueDropdown("@" + Colors.GetDataListPath), OnValueChanged(nameof(OnColorChanged))]
#endif
    [SerializeField] protected int colorId;


    public virtual int ColorId
    {
        get => colorId;
        set
        {
            colorId = value;
            Color color = Colors.Instance.GetColor(colorId);

            foreach (Renderer renderer in renderers)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    renderer.sharedMaterial.color = color;
                else
#endif
                    renderer.material.color = color;
            }
            this.Dirty();
        }
    }

    private void OnColorChanged()
    {
        ColorId = colorId;
    }

    protected virtual void Awake()
    {
        if (randomColor)
            colorId = Colors.Instance.RandomId;

        ColorId = colorId;
    }
}
