using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    public event Action<Health> PlayerSpawned;
    public event Action<Health> PlayerDespawned;

    private Health _playerHealth;
    public Player Player { get; private set; }

    private void Start()
    {
        SpawnActivePlayer();
    }

    private void OnDisable()
    {
        UnsubscribeFromPlayer();
    }

    private void SpawnActivePlayer()
    {
        Player = PoolManager.Instance.PlayerPool.Spawn(_spawnPoint.position, _spawnPoint.rotation);

        if (Player.TryGetComponent<Health>(out Health health))
        {
            _playerHealth = health;
            _playerHealth.Dying += HandlePlayerDeath;
            PlayerSpawned?.Invoke(health);
        }
    }

    private void UnsubscribeFromPlayer()
    {
        if (_playerHealth != null)
            _playerHealth.Dying -= HandlePlayerDeath;

        _playerHealth = null;
    }

    private void HandlePlayerDeath(Health health)
    {
        UnsubscribeFromPlayer();
        PlayerDespawned?.Invoke(health);
        Player.NotifyDying();

    }

    public void ResetPlayer()
    {
        if (Player != null && Player.isActiveAndEnabled)
        {
            if (_playerHealth != null)
            {
                HandlePlayerDeath(_playerHealth);
            }
        }
        else
        {
            UnsubscribeFromPlayer();
        }

        SpawnActivePlayer();
    }
}