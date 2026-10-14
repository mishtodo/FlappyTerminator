using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _startDelay = 2f;
    [SerializeField] private float _spawnInterval = 2.5f;
    [SerializeField] private float _minSpawnY = -3f;
    [SerializeField] private float _maxSpawnY = 4f;
    [SerializeField] private float _waveSize = 3f;

    private Coroutine _spawnCoroutine;
    private int _spawnedInCurrentWave = 0;
    private int _currentEnemyIndex = 0;

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private void OnDestroy()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
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

        float randomY = Random.Range(_minSpawnY, _maxSpawnY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0f);

        Enemy newEnemy = pools[_currentEnemyIndex].Spawn(spawnPosition, Quaternion.identity);

        _spawnedInCurrentWave++;

        if (_spawnedInCurrentWave >= _waveSize)
        {
            _spawnedInCurrentWave = 0;
            _currentEnemyIndex = (_currentEnemyIndex + 1) % pools.Count;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 from = new Vector3(transform.position.x, _minSpawnY, 0f);
        Vector3 to = new Vector3(transform.position.x, _maxSpawnY, 0f);
        Gizmos.DrawLine(from, to);
    }
}