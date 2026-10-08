using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover))]
[RequireComponent(typeof(PlayerShooter), typeof(Laserer))]
public class Player : SpawnableObject
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private PlayerShooter _playerShooter;
    [SerializeField] private Laserer _laserer;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _playerShooter = GetComponent<PlayerShooter>();
        _laserer = GetComponent<Laserer>();
        //_groundDetector = GetComponent<GroundDetector>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.GetIsShooting())
            _playerShooter.Shoot();

        if (_inputReader.GetIsLasering())
            _laserer.ShootLaser();

        //Variant A
        //if (_inputReader.GetIsJump())
        //{
        //    _mover.Jump();
        //}
        //else if (_inputReader.GetIsJumpCanceled())
        //{
        //    _mover.CancelJump();
        //}
        //
        //_mover.Fall();

        //Variant B
        if (_inputReader.GetIsJump())
            _mover.Jump();
        else if (_inputReader.GetIsJumpCanceled())
            _mover.CancelJump();
        else
            _mover.Fall();
    }
}