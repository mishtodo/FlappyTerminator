using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _max = 100;
    [SerializeField] private int _current;

    public int Max => _max;

    public event Action<int> Changed;
    public event Action<Health> Dying;

    private void Start()
    {
        _current = _max;
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _current -= damage;
            _current = Mathf.Max(_current, 0);
            Changed?.Invoke(_current);

            if (_current <= 0)
            {
                Dying?.Invoke(this);
            }
        }
    }

    public void TakeHeal(int heal)
    {
        if (heal > 0)
        {
            _current += heal;
            _current = Mathf.Min(_current, _max);
            Changed?.Invoke(_current);
        }
    }
}