using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Rigidbody2D), typeof(Health))]
public class Enemy : SpawnableObject
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Health _health;
    [SerializeField] private float _direcion = -1.0f;
    [SerializeField] private float _speedX = 150.0f;

    private Rigidbody2D _rb;

    protected override void OnEnable()
    {
        _health.Dying += HandleDeath;
    }

    protected override void OnDisable()
    {
        _health.Dying -= HandleDeath;
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rb = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        _mover.Move(_direcion, _speedX);

        //if (_vision.IsSeeing)
        //{
        //    if (_follower.IsFollow(_vision.GetTargetTransform()))
        //    {
        //        _rotator.RotateTowards(_vision.GetTargetTransform());
        //        _mover.Move(_follower.GetDirection(_vision.GetTargetTransform()));
        //    }
        //    else
        //    {
        //        if (_damager.CanAttack(_vision.GetTargetTransform(), out Health health))
        //            _damager.Attack(health);
        //    }
        //}
        //else
        //{
        //    _rotator.RotateTowards(_patroler.GetPatrolPoint());
        //    _mover.Move(_patroler.GetDirection());
        //}
    }

    private void HandleDeath(Health health)
    {
        Debug.Log("Dead as F...");
        NotifyDying();
    }
}