using System.Collections;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private float _elapsed;

    private Coroutine _coroutine;


    public void StartCounting()
    {
        Stop();
        text.text = string.Empty;
        _elapsed = 0;
        _coroutine = StartCoroutine(Counting());
    }

    public void Stop()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }


    private IEnumerator Counting()
    {
        while (true)
        {
            yield return Waiters.Second;
            _elapsed++;
            text.text = _elapsed.ToString();
        }
    }
}
