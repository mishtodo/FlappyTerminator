using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private PlayerBullet _playerBulletPrefab;
    [SerializeField] private EnemyBullet _enemyBulletPrefab;

    public PlayerPool PlayerPool { get; private set; }
    public List<EnemyPool> EnemyPools { get; private set; } = new List<EnemyPool>();
    public PlayerBulletPool PlayerBulletPool { get; private set; }
    public EnemyBulletPool EnemyBulletPool { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitPools();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitPools()
    {
        GameObject playerPoolContainer = new GameObject("Container_Players");
        GameObject enemyPoolContainer = new GameObject("Container_Enemies");
        GameObject playerBulletContainer = new GameObject("Container_PlayerBullets");
        GameObject enemyBulletContainer = new GameObject("Container_EnemyBullets");

        playerPoolContainer.transform.SetParent(transform);
        enemyPoolContainer.transform.SetParent(transform);
        playerBulletContainer.transform.SetParent(transform);
        enemyBulletContainer.transform.SetParent(transform);

        PlayerPool = playerPoolContainer.AddComponent<PlayerPool>();
        PlayerPool.Initialize(_playerPrefab, playerPoolContainer);

        for (int i = 0; i < _enemyPrefabs.Length; i++)
        {
            if (_enemyPrefabs[i] == null) continue;

            EnemyPool newPool = enemyPoolContainer.AddComponent<EnemyPool>();
            newPool.Initialize(_enemyPrefabs[i], enemyPoolContainer);
            EnemyPools.Add(newPool);
        }

        PlayerBulletPool = playerBulletContainer.AddComponent<PlayerBulletPool>();
        PlayerBulletPool.Initialize(_playerBulletPrefab, playerBulletContainer);

        EnemyBulletPool = enemyBulletContainer.AddComponent<EnemyBulletPool>();
        EnemyBulletPool.Initialize(_enemyBulletPrefab, enemyBulletContainer);
    }
}