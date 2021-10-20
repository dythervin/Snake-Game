using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ResourceAmount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private bool combo;
    [SerializeField] private string prefix;

    private const string ComboName = "Combo";

    [BoxGroup(ComboName, VisibleIf = nameof(combo))]
    [SerializeField] private float comboDuration;

    [BoxGroup(ComboName)]
    [SerializeField] private int targetCombo = 3;

    [BoxGroup(ComboName)]
    [SerializeField] private UnityEvent targetComboReached;

    private int _comboCount;
    private int _current;
    private float _lastComboTime;

    private int Current
    {
        get => _current;
        set
        {
            _current = value;
            text.text = $"{prefix}{value}";
        }
    }

    public void Increment()
    {
        ++Current;

        if (combo)
            IncrementCombo();
    }

    private void IncrementCombo()
    {
        if (_lastComboTime + comboDuration <= Time.time)
            _comboCount = 1;
        else
            _comboCount++;

        if (_comboCount >= targetCombo)
            targetComboReached.Invoke();

        _lastComboTime = Time.time;
    }

    public void SetToZero()
    {
        Current = 0;
        _comboCount = 0;
    }
}
