using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover))]
[RequireComponent(typeof(Shooter), typeof(Laserer))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private Shooter _shooter;
    [SerializeField] private Laserer _laserer;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _shooter = GetComponent<Shooter>();
        _laserer = GetComponent<Laserer>();
        //_groundDetector = GetComponent<GroundDetector>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.GetIsShooting())
            _shooter.Shoot();

        if (_inputReader.GetIsLasering())
            _laserer.ShootLaser();

        if (_inputReader.GetIsJump())
            _mover.Jump();
        else
            _mover.Fall();
    }
}