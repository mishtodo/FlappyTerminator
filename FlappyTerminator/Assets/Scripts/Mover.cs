using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    //[SerializeField] private float _speedX = 250f;
    [SerializeField] private float _jumpForce = 250f;
    [SerializeField] private float _tapForce = 25f;
    [SerializeField] private float _rotationSpeed = 1.5f;
    [SerializeField] private float _minRotationZ = -45f;
    [SerializeField] private float _maxRotationZ = 45f;

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
        _rigidbody.AddForce(new Vector2(0, _jumpForce));
        _rigidbody.MoveRotation(_maxRotation);
    }

    public void Fall()
    {
        Quaternion nextRotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(nextRotation);
    }

    //public void Move(float direction)
    //{
    //    _rigidbody.velocity = new Vector2(_speedX * direction * Time.fixedDeltaTime, _rigidbody.velocity.y);
    //}
}