using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;

    void Start()
    {
        _enemySpawner.Spawn(_enemySpawner.transform.position, _enemySpawner.transform.rotation);
    }
}