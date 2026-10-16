using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    public event Action<Health> PlayerSpawned;
    public event Action<Health> PlayerDespawned;

    private Health _playerHealth;
    public Player Player { get; private set; }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        if (_playerHealth != null)
            _playerHealth.Dying -= HandlePlayerDeath;
    }

    private void Start()
    {
        Player = PoolManager.Instance.PlayerPool.Spawn(_spawnPoint.position, _spawnPoint.rotation);

        if (Player.TryGetComponent<Health>(out Health health))
        {
            PlayerSpawned?.Invoke(health);
            _playerHealth = health;
            _playerHealth.Dying += HandlePlayerDeath;
        }
    }

    private void HandlePlayerDeath(Health health)
    {
        PlayerDespawned?.Invoke(health);
        Player.NotifyDying();
    }

    public void ResetPlayer()
    {
        if (!Player.isActiveAndEnabled)
        {
            Player = PoolManager.Instance.PlayerPool.Spawn(_spawnPoint.position, _spawnPoint.rotation);
        }
        else
        {
            Player.NotifyDying();
            Player = PoolManager.Instance.PlayerPool.Spawn(_spawnPoint.position, _spawnPoint.rotation);
        }
    }
}