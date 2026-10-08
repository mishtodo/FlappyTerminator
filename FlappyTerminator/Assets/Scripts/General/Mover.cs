using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    public enum RotationMode
    {
        ImmediateFall,
        OnVelocityDown
    }

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private RotationMode _currentRotationMode = RotationMode.OnVelocityDown;
    [SerializeField] private float _jumpForce = 8f;
    [SerializeField] private float _rotationSpeed = 1.5f;
    [SerializeField] private float _minRotationZ = -45f;
    [SerializeField] private float _maxRotationZ = 45f;

    [Range(0f, 1f)]
    [SerializeField] private float _jumpCutMultiplier = 0.25f;

    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private void Start()
    {
        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        _rigidbody.MoveRotation(_maxRotation);
    }

    public void CancelJump()
    {
        if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * _jumpCutMultiplier);
        }
    }

    public void Fall()
    {
        switch (_currentRotationMode)
        {
            case RotationMode.ImmediateFall:
                ApplyDownwardRotation();
                break;

            case RotationMode.OnVelocityDown:
                if (_rigidbody.velocity.y < 0)
                    ApplyDownwardRotation();
                break;
        }
    }

    public void SetRotationMode(RotationMode newMode)
    {
        _currentRotationMode = newMode;
    }

    public void SetRotationModeIndex(int index)
    {
        _currentRotationMode = (RotationMode)index;
    }

    public void Move(float direction, float speedX)
    {
        _rigidbody.velocity = new Vector2(speedX * direction * Time.fixedDeltaTime, _rigidbody.velocity.y);
    }

    private void ApplyDownwardRotation()
    {
        Quaternion nextRotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(nextRotation);
    }
}