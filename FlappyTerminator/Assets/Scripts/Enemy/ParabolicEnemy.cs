using UnityEngine;

[RequireComponent(typeof(ParabolicMover))]
public class ParabolicEnemy : Enemy
{
    [SerializeField] private ParabolicMover _parabolicMover;

    protected override void Awake()
    {
        base.Awake();
        _parabolicMover = GetComponent<ParabolicMover>();
    }

    private void FixedUpdate()
    {
        _parabolicMover.Move();
    }
}