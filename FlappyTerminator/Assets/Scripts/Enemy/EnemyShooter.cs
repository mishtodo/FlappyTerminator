using System.Collections;
using UnityEngine;

public class EnemyShooter : Shooter
{
    [SerializeField] private float _shootInterval = 1f;
    [SerializeField] private float _enemyBulletSpeed = 10f;

    private Vector2 _shootDirection = Vector2.left;
    private Coroutine _shootCoroutine;

    private void OnEnable()
    {
        _shootCoroutine = StartCoroutine(ShootRoutine());
    }

    private void OnDisable()
    {
        if (_shootCoroutine != null)
        {
            StopCoroutine(_shootCoroutine);
        }
    }

    private IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(_shootInterval);

        while (true)
        {
            Shoot(_enemyBulletSpeed, _shootDirection);
            yield return new WaitForSeconds(_shootInterval);
        }
    }

    protected override SpawnableObject GetBulletFromPool()
    {
        return PoolManager.Instance.EnemyBulletPool.Spawn(FirePoint.position, FirePoint.transform.parent.rotation);
    }
}
