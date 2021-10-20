using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Snake snakePrefab;

    private Level CurrentLevel => LevelManager.Instance.Current;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        LevelManager.Instance.Load();
    }

    public void RestartGame()
    {
        LevelManager.Instance.Load();
    }





    public void SpawnSnake()
    {
        Vector3 snakePosition = CurrentLevel.SnakeSpawnPosition;
        Instantiate(snakePrefab, snakePosition, Quaternion.identity, CurrentLevel.transform);
    }
}
