using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _startDelay = 2f;
    [SerializeField] private float _spawnInterval = 2.5f;
    [SerializeField] private float _minSpawnY = -3f;
    [SerializeField] private float _maxSpawnY = 4f;

    private Coroutine _spawnCoroutine;

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

        float randomY = Random.Range(_minSpawnY, _maxSpawnY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0f);
        Enemy newEnemy = PoolManager.Instance.EnemyPool.Spawn(spawnPosition, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 from = new Vector3(transform.position.x, _minSpawnY, 0f);
        Vector3 to = new Vector3(transform.position.x, _maxSpawnY, 0f);
        Gizmos.DrawLine(from, to);
    }
}