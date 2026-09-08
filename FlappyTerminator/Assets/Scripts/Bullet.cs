using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : SpawnableObject
{
    [SerializeField] private int _bulletDamage = 20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _) == false)
        {
            if (collision.gameObject.TryGetComponent<Health>(out Health resource))
                resource.TakeDamage(_bulletDamage);

            NotifyDying();
        }
    }
}