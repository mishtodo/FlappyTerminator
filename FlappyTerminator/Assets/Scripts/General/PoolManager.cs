using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [Header("Префабы объектов")]
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private PlayerBullet _playerBulletPrefab;
    [SerializeField] private EnemyBullet _enemyBulletPrefab;

    public PlayerPool PlayerPool { get; private set; }
    public EnemyPool EnemyPool { get; private set; }
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

        EnemyPool = enemyPoolContainer.AddComponent<EnemyPool>();
        EnemyPool.Initialize(_enemyPrefab, enemyPoolContainer);

        PlayerBulletPool = playerBulletContainer.AddComponent<PlayerBulletPool>();
        PlayerBulletPool.Initialize(_playerBulletPrefab, playerBulletContainer);

        EnemyBulletPool = enemyBulletContainer.AddComponent<EnemyBulletPool>();
        EnemyBulletPool.Initialize(_enemyBulletPrefab, enemyBulletContainer);
    }
}