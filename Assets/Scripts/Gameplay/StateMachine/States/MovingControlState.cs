using UnityEngine;

public class MovingControlState : MovingState
{
    private float _xRange;
    private float _xStart;

    public override void Init(Snake snake)
    {
        base.Init(snake);
        _xRange = Mathf.Abs(LevelManager.Instance.Current.XRange - snake.Radius);
    }

    public override void Enter()
    {
        base.Enter();
        PlayerController.Instance.OnXValueChanged += OnDrag;
        ;
        ;
        PlayerController.Instance.OnDragBegin += OnDragBegin;
    }

    private void OnDrag(float x, float intensity, PlayerController.InputType inputType)
    {
        float targetX = inputType switch
        {
            PlayerController.InputType.ScreenPosition => (x - intensity * .5f) * _xRange,
            _ => _xStart + x * _xRange * 2
        };

        Movement.TargetX = Mathf.Clamp(targetX, -_xRange, _xRange);
    }

    public override void Exit()
    {
        PlayerController.Instance.OnXValueChanged -= OnDrag;
        PlayerController.Instance.OnDragBegin -= OnDragBegin;
        base.Exit();
    }

    private void OnDragBegin()
    {
        _xStart = Movement.HeadPosition.x;
    }


}
