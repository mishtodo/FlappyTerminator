using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Enemy enemy = Spawn(transform.position, transform.rotation);
        //enemy.OnDying += HandleEnemyDeath;
    }

    //private void HandleEnemyDeath(SpawnableObject enemy)
    //{
    //    enemy.OnDying -= HandleEnemyDeath;

    //}

    //private void DestroyEnemy(Health health)
    //{
    //    health.Dying -= DestroyEnemy;
    //    if (health.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
    //    {
    //        Debug.Log("Dead as F...");
    //        enemy.NotifyDying();
    //    }
    //}
}