using UnityEngine;
using System;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int health;
    public event Action<int> OnHealthChanged;

    public int CurrentHealth => health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
        
        OnHealthChanged?.Invoke(health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle death logic here
        Debug.Log(gameObject.name + " has died.");
    }
}
