using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;


    public void Enable(bool value)
    {
        canvasGroup.alpha = value ? 1 : 0;
        canvasGroup.blocksRaycasts = value;
        canvasGroup.interactable = value;
    }
}
