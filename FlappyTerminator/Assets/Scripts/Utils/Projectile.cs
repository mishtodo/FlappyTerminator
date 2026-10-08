using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : SpawnableObject
{
    [SerializeField] protected int Damage = 1;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health targetHealth))
        {
            ApplyDamage(targetHealth);
        }

        NotifyDying();
    }

    protected virtual void ApplyDamage(Health health)
    {
        health.TakeDamage(Damage);
    }
}