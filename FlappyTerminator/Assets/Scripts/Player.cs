using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(Shooter))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private Shooter _shooter;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _shooter = GetComponent<Shooter>();
        //_groundDetector = GetComponent<GroundDetector>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.GetIsShooting())
            _shooter.Shoot();

        if (_inputReader.GetIsJump())
            _mover.Jump();
    }
}