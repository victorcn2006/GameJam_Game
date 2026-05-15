using UnityEngine;
using StateMachine.Runtime;
using UnityEngine.Events;

public class BossController : MonoBehaviour
{
    const string idleStateString = "Idle";
    const string attackStateString = "Attack";
    const string vulnerableStateString = "Vulnerable";
    const string dieStateString = "Die";

    StateMachineComponent sm;
    TimeManager tm;

    [Header("Boss Stats")]
    [SerializeField] int maxHP = 3;
    [SerializeField] int currentHP;

    [Header("Boss State")]
    [SerializeField] bool isDead = false;
    [SerializeField] bool invulnerable = false;

    [Header("References")]
    [SerializeField] GameObject LaserLightPrefab;
    [SerializeField] Transform LaserLightTransform;

    [Header("Player")]
    [SerializeField] Transform player;

    [Header("Events")]
    private UnityEvent OnTakeDamage;
    private UnityEvent OnDeath;
    private UnityEvent OnHeal;

    void Start()
    {
        sm = GetComponent<StateMachineComponent>();
        tm = TimeManager.Instance;
        currentHP = maxHP;
    }

    string previousState = "";

    void Update()
    {
        string currentState = sm.GetCurrentStateName();

        // Detectar cambio de estado
        if (currentState != previousState)
        {
            previousState = currentState;

            switch (currentState)
            {
                case idleStateString:

                    tm.OneShotTimer(0.5f, () =>
                    {
                        sm.GetStateContext().idleChangeState = true;
                    });

                    break;

                case attackStateString:

                    // SOLO se ejecuta una vez al entrar
                    ShootLaser();

                    break;

                case vulnerableStateString:

                    tm.OneShotTimer(1f, () =>
                    {
                        sm.GetStateContext().vulnerableChangeState = true;
                    });

                    break;

                case dieStateString:
                    break;
            }
        }
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

    public void ShootLaser()
    {
        if (LaserLightPrefab == null || LaserLightTransform == null || player == null)
            return;

        // Dirección hacia el jugador
        Vector3 direction = (player.position - LaserLightTransform.position).normalized;

        // Rotación mirando al jugador
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Instanciar láser
        GameObject laser = Instantiate(
            LaserLightPrefab,
            LaserLightTransform.position,
            rotation
        );
        sm.GetStateContext().vulnerableChangeState = true;
        //timerStarted = false;
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