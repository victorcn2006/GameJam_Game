using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _hearts;
    [SerializeField] private CharacterStats _characterStats;

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
    }
}
