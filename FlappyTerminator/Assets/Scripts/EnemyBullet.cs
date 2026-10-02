using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : SpawnableObject
{
    [SerializeField] private int _bulletDamage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health resource))
            resource.TakeDamage(_bulletDamage);

        NotifyDying();
    }
}