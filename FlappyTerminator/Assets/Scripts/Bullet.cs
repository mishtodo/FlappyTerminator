using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _bulletDamage = 20;

    private Rigidbody2D _rb;

    public event Action<Bullet> Dying;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _) == false)
        {
            if (collision.gameObject.TryGetComponent<Health>(out Health resource))
                resource.TakeDamage(_bulletDamage);

            Dying?.Invoke(this);
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void InitializeVelocity(Vector3 velocity)
    {
        _rb.velocity = velocity;
    }

    public void InitializePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void InitializeRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}