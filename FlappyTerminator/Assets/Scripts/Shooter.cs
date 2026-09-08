using UnityEngine;

public class Shooter : Spawner<Bullet>
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletSpeed = 13f;

    public void Shoot()
    {
        Bullet nextBullet = Spawn(_firePoint.position, _firePoint.transform.parent.rotation);
        nextBullet.InitializeVelocity(_firePoint.right * _bulletSpeed);
    }
}