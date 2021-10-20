using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Window[] windows;

    private void Awake()
    {
        SetWindowsEnabled(false);
    }

    public void SetWindowsEnabled(bool value)
    {
        foreach (Window window in windows)
        {
            window.Enable(value);
        }
    }
}
