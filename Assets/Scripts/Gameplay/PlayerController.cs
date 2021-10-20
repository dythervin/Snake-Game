using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : Singleton<PlayerController>, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private float intensity = 2;
    [SerializeField] InputType inputType;
    public event System.Action<float, float, InputType> OnXValueChanged;
    public event System.Action OnDragBegin;
    public event System.Action OnDragEnd;
    private float _screenWidth;

    private float _startPoint;

    private void Start()
    {
        _screenWidth = Screen.width;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        float currentX = GetTargetX(eventData.position.x);
        OnXValueChanged?.Invoke(currentX, intensity, inputType);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _startPoint = eventData.position.x;
        OnDragBegin?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnDragEnd?.Invoke();
    }

    private float GetTargetX(in float x)
    {
        float targetX = inputType switch
        {
            InputType.ScreenPosition => x,
            _ => (x - _startPoint)
        };

        return targetX * intensity / _screenWidth;
    }

    public enum InputType
    {
        ScreenPosition,
        Delta
    }
}
