using UnityEngine;

[RequireComponent(typeof(LinearMover))]
public class LinearEnemy : Enemy
{
    [SerializeField] private LinearMover _linearMover;

    protected override void Awake()
    {
        base.Awake();
        _linearMover = GetComponent<LinearMover>();
    }

    void FixedUpdate()
    {
        _linearMover.Move();
    }
}