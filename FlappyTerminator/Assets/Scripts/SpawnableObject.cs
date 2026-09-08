using System;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour
{
    public event Action<SpawnableObject> OnDying;

    protected virtual void OnEnable() { }

    public void InitializeVelocity(Vector2 value)
    {
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = value;
        }
    }

    public void InitializeAnguilarVelocity(float value)
    {
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.angularVelocity = value;
        }
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void InitializePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void InitializeRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void NotifyDying()
    {
        OnDying?.Invoke(this);
    }
}