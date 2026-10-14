using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Health))]
public class Enemy : SpawnableObject
{
    [SerializeField] private Health _health;

    protected override void OnEnable()
    {
        _health.Dying += HandleDeath;
    }

    protected override void OnDisable()
    {
        _health.Dying -= HandleDeath;
    }

    protected virtual void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void HandleDeath(Health health)
    {
        NotifyDying();
    }
}