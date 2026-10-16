using System;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button Button;

    public event Action Clicked;

    private void OnEnable()
    {
        Button.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(RestartGame);
    }

    private void RestartGame()
    {
        Clicked?.Invoke();
    }

    public void SetEnable()
    {
        this.gameObject.SetActive(true);
    }

    public void SetDisable()
    {
        this.gameObject.SetActive(false);
    }
}