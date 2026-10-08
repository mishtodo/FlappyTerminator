using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    [SerializeField] protected Transform FirePoint;
    [SerializeField] protected float DefaultBulletSpeed = 13f;

    public void Shoot(float speed, Vector2 direction)
    {
        if (FirePoint == null || PoolManager.Instance == null) return;

        SpawnableObject nextBullet = GetBulletFromPool();

        if (nextBullet != null)
        {
            nextBullet.InitializePosition(FirePoint.position);
            nextBullet.InitializeRotation(FirePoint.transform.parent.rotation);

            Vector2 finalVelocity = direction.normalized * speed;
            nextBullet.InitializeVelocity(finalVelocity);
        }
    }

    public void Shoot()
    {
        Shoot(DefaultBulletSpeed, FirePoint.right);
    }

    protected abstract SpawnableObject GetBulletFromPool();
}
