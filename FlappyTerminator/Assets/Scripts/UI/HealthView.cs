using TMPro;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private PlayerSpawner _playerSpawner;

    private Health _activeHealth;

    private void OnEnable()
    {
        _playerSpawner.PlayerSpawned += HandleHealth;
        _playerSpawner.PlayerDespawned += HandleDespawn;
    }

    private void OnDisable()
    {
        UnsubscribeHealth();
        _playerSpawner.PlayerSpawned -= HandleHealth;
        _playerSpawner.PlayerDespawned -= HandleDespawn;
    }

    private void HandleDespawn(Health health)
    {
        UnsubscribeHealth();
        _activeHealth = null;
        //_healthText.text = "";
    }

    private void HandleHealth(Health health)
    {
        UnsubscribeHealth();

        _activeHealth = health;
        _activeHealth.Changed += UpdateHealth;
        UpdateHealth(_activeHealth.Max);
    }

    private void UnsubscribeHealth()
    {
        if (_activeHealth != null)
        {
            _activeHealth.Changed -= UpdateHealth;
            _activeHealth = null;
        }
    }

    public void UpdateHealth(int current)
    {
        _healthText.text = current.ToString();
    }
}