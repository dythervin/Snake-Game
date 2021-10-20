using UnityEngine;
using UnityEngine.Events;

public class Finish : MonoBehaviour
{
    [SerializeField] private UnityEvent onPlayerEnter;

    private bool _gameOver;

    private void OnTriggerEnter(Collider other)
    {
        if (_gameOver)
            return;

        if (other.CompareTag(Tags.Player))
        {
            onPlayerEnter.Invoke();
        }
    }

    public void GameOver()
    {
        _gameOver = true;
    }
}
