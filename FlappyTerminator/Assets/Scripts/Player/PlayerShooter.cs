using UnityEngine;

public class PlayerShooter : Shooter
{
    protected override SpawnableObject GetBulletFromPool()
    {
        return PoolManager.Instance.PlayerBulletPool.Spawn(FirePoint.position, FirePoint.transform.parent.rotation);
    }
}