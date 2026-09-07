using UnityEngine;
using UnityEngine.Pool;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private GameObject _objectPool;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletSpeed = 13f;

    private ObjectPool<Bullet> _bulletsPool;
    private int _poolDefaultCapacity = 10;
    private int _poolMaxCapacity = 20;

    private void Awake()
    {
        _bulletsPool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity),
            actionOnGet: (bullet) => ActionOnGet(bullet),
            actionOnRelease: (bullet) => ActionOnRelease(bullet),
            actionOnDestroy: (bullet) => Destroy(bullet.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolDefaultCapacity,
            maxSize: _poolMaxCapacity);
    }

    public void Shoot()
    {
        GetBullet();
    }

    private void ActionOnGet(Bullet bullet)
    {
        bullet.Dying += ReleaseBullet;
        bullet.InitializePosition(_firePoint.position);
        bullet.Activate();

        if (_firePoint.parent.rotation == Quaternion.identity)
            bullet.InitializeVelocity(_bulletSpeed * Vector3.right);
        else
            bullet.InitializeVelocity(_bulletSpeed * Vector3.left);
    }

    private void ActionOnRelease(Bullet bullet)
    {
        bullet.Dying -= ReleaseBullet;
        bullet.InitializePosition(_objectPool.transform.position);
        bullet.InitializeRotation(_objectPool.transform.rotation);
        bullet.Deactivate();
    }

    private void ReleaseBullet(Bullet bullet)
    {
        _bulletsPool.Release(bullet);
    }

    private void GetBullet()
    {
        _bulletsPool.Get();
    }
}