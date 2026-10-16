using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerMover))]
[RequireComponent(typeof(PlayerShooter), typeof(Laserer), typeof(Health))]
public class Player : SpawnableObject
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerShooter _playerShooter;
    [SerializeField] private Laserer _laserer;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerMover = GetComponent<PlayerMover>();
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
            _playerMover.Jump();
        else if (_inputReader.GetIsJumpCanceled())
            _playerMover.CancelJump();
        else
            _playerMover.Fall();
    }
}