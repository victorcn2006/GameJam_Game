using UnityEngine;
using StateMachine.Runtime;
using UnityEngine.Events;

public class BossController : MonoBehaviour
{
    StateMachineComponent sm;

    [Header("Boss Stats")]
    [SerializeField] int maxHP = 3;
    [SerializeField] int currentHP;

    [Header("Boss State")]
    [SerializeField] bool isDead = false;
    [SerializeField] bool invulnerable = false;

    [Header("Events")]
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;
    public UnityEvent OnHeal;

    void Start()
    {
        sm = GetComponent<StateMachineComponent>();

        currentHP = maxHP;
    }

    void Update()
    {
        string currentState = sm.GetCurrentStateName();
    }

    // DAMAGE

    public void TakeDamage(int damage)
    {
        // Si está muerto o es invulnerable no recibe daño
        if (isDead || invulnerable)
            return;

        currentHP -= damage;

        Debug.Log("Boss recibió " + damage + " de daño");

        OnTakeDamage?.Invoke();

        // Evitar vida negativa
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    // DEATH

    void Die()
    {
        if (isDead)
            return;

        isDead = true;

        Debug.Log("Boss derrotado");

        OnDeath?.Invoke();
    }

    // INVULNERABILITY

    public void SetInvulnerable(bool value)
    {
        invulnerable = value;
    }

    // GETTERS

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public int GetMaxHP()
    {
        return maxHP;
    }

    public bool IsDead()
    {
        return isDead;
    }

    // RESET BOSS

    public void ResetBoss()
    {
        isDead = false;
        currentHP = maxHP;
        invulnerable = false;
    }
}