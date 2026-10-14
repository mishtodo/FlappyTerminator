using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ParabolicMover : MonoBehaviour
{
    [SerializeField] private float _speedX = 3f;
    [SerializeField] private float _frequency = 0.5f;
    [SerializeField] private float _amplitude = 2f;

    private Rigidbody2D _rb;
    private float _startX;
    private float _startY;
    private float _direction = -1.0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _startX = transform.position.x;
        _startY = transform.position.y;
    }

    public void Move()
    {
        float newX = _rb.position.x + (_speedX * _direction * Time.fixedDeltaTime);
        float elapsedDistance = Mathf.Abs(newX - _startX);

        float newY = _startY + Mathf.Sin(elapsedDistance * _frequency) * _amplitude;

        _rb.MovePosition(new Vector2(newX, newY));
    }
}