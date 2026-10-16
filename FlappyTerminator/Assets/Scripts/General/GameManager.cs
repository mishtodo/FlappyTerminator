using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private RestartButton _restartButton;

    private void OnEnable()
    {
        _playerSpawner.PlayerDespawned += PauseGame;
        _restartButton.Clicked += RestartGame;
    }

    private void OnDisable()
    {
        _playerSpawner.PlayerDespawned -= PauseGame;
        _restartButton.Clicked -= RestartGame;
    }

    private void PauseGame(Health _)
    {
        Time.timeScale = 0;
        _restartButton.SetEnable();
    }

    private void RestartGame()
    {
        _restartButton.SetDisable();
        _enemySpawner.ResetEnemies();
        _playerSpawner.ResetPlayer();
        Time.timeScale = 1;
    }
}