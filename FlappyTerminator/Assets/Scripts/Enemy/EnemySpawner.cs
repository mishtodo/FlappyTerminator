using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _startDelay = 2f;
    [SerializeField] private float _spawnInterval = 2.5f;
    [SerializeField] private float _minSpawnY = -3f;
    [SerializeField] private float _maxSpawnY = 4f;
    [SerializeField] private float _waveSize = 3f;

    public event Action<int> MetricsChanged;

    public int DespawnCounts { get; private set; }

    private Coroutine _spawnCoroutine;
    private int _spawnedInCurrentWave = 0;
    private int _currentEnemyIndex = 0;

    private readonly List<Enemy> _activeEnemies = new List<Enemy>();

    private void OnDisable()
    {
        StopSpawnRoutine();
        ClearAllActiveEnemies();
    }

    private void Start()
    {
        StartSpawnRoutine();
    }

    private void StartSpawnRoutine()
    {
        StopSpawnRoutine();
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private void StopSpawnRoutine()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private void OnDestroy()
    {
        StopSpawnRoutine();
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(_startDelay);

        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (PoolManager.Instance == null) return;

        var pools = PoolManager.Instance.EnemyPools;
        if (pools.Count == 0) return;

        float randomY = UnityEngine.Random.Range(_minSpawnY, _maxSpawnY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0f);

        Enemy newEnemy = pools[_currentEnemyIndex].Spawn(spawnPosition, Quaternion.identity);
        _activeEnemies.Add(newEnemy);

        if (newEnemy.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.Dying += HandleEnemyDeath;
        }

        _spawnedInCurrentWave++;

        if (_spawnedInCurrentWave >= _waveSize)
        {
            _spawnedInCurrentWave = 0;
            _currentEnemyIndex = (_currentEnemyIndex + 1) % pools.Count;
        }
    }

    private void HandleEnemyDeath(Health health)
    {
        health.Dying -= HandleEnemyDeath;

        DespawnCounts++;
        MetricsChanged?.Invoke(DespawnCounts);

        if (health.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _activeEnemies.Remove(enemy);
            enemy.NotifyDying();
        }
    }

    private void ClearAllActiveEnemies()
    {
        for (int i = _activeEnemies.Count - 1; i >= 0; i--)
        {
            Enemy enemy = _activeEnemies[i];
            if (enemy != null)
            {
                if (enemy.TryGetComponent<Health>(out Health health))
                {
                    health.Dying -= HandleEnemyDeath;
                }

                enemy.NotifyDying();
            }
        }

        _activeEnemies.Clear();
    }


    public void ResetEnemies()
    {
        StopSpawnRoutine();

        ClearAllActiveEnemies();

        DespawnCounts = 0;
        _spawnedInCurrentWave = 0;
        _currentEnemyIndex = 0;
        MetricsChanged?.Invoke(DespawnCounts);

        StartSpawnRoutine();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 from = new Vector3(transform.position.x, _minSpawnY, 0f);
        Vector3 to = new Vector3(transform.position.x, _maxSpawnY, 0f);
        Gizmos.DrawLine(from, to);
    }
}