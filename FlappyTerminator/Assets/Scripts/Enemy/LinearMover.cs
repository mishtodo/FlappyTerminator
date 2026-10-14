using UnityEngine;

public class LinearMover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speedX = 150.0f;
    [SerializeField] private float _direction = -1.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        _rigidbody.velocity = new Vector2(_speedX * _direction * Time.fixedDeltaTime, _rigidbody.velocity.y);
    }
}