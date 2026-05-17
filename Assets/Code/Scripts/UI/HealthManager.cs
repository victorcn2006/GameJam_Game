using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _hearts;
    [SerializeField] private CharacterStats _characterStats;

    [Header("Death Events")]
    [SerializeField] private UnityEvent _onDeath;
    [SerializeField] private GameObject _deathPanel;
    [SerializeField] private string _mainMenuSceneName = "MainMenu";

    private bool _isDead = false;

    private void OnEnable()
    {
        if (_characterStats != null)
        {
            _characterStats.OnHealthChanged += UpdateHearts;
        }
    }

    private void OnDisable()
    {
        if (_characterStats != null)
        {
            _characterStats.OnHealthChanged -= UpdateHearts;
        }
    }

    private void Start() {
        InitializeHearts();

        if (_deathPanel != null) _deathPanel.SetActive(false);

        if (_characterStats != null) {
            UpdateHearts(_characterStats.CurrentHealth);
        } else {
            Debug.LogWarning("Character Stats References is missing in " + this.name + "!");
        }
    }

    private void InitializeHearts() {
        foreach (var heart in _hearts) {
            if (!heart.activeInHierarchy) {
                heart.SetActive(true);
            }
        }
    }

    private void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            _hearts[i].SetActive(i < currentHealth);
        }

        if (currentHealth <= 0 && !_isDead)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        _isDead = true;
        
        if (_deathPanel != null)
        {
            _deathPanel.SetActive(true);
        }

        _onDeath?.Invoke();

        StartCoroutine(ReturnToMenuRoutine());
    }

    private IEnumerator ReturnToMenuRoutine()
    {
        yield return new WaitForSecondsRealtime(3f);
        
        // Ensure time scale is reset if the game was paused
        Time.timeScale = 1f;
        SceneManager.LoadScene(_mainMenuSceneName);
    }
}
