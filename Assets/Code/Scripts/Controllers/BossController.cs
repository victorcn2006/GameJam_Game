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

    [Header("Timings")]
    [SerializeField] float idleTime = 2f;
    [SerializeField] float vulnerableTime = 1f;

    [Header("Events")]
    private UnityEvent OnTakeDamage;
    private UnityEvent OnDeath;
    private UnityEvent OnHeal;

    string previousState = "";

    void Start()
    {
        sm = GetComponent<StateMachineComponent>();
        tm = TimeManager.Instance;

        currentHP = maxHP;
    }

    void Update()
    {
        string currentState = "";
        if (sm.execution)
            currentState = sm.GetCurrentStateName();

        if (currentState == previousState)
            return;

        previousState = currentState;

        switch (currentState)
        {
            case idleStateString:
                EnterIdle();
                break;

            case attackStateString:
                EnterAttack();
                break;

            case vulnerableStateString:
                EnterVulnerable();
                break;

            case dieStateString:
                EnterDie();
                break;
        }
    }

    void EnterIdle()
    {
        tm.OneShotTimer(idleTime, () =>
        {
            sm.GetStateContext().idleChangeState = true;
        });
    }

    void EnterAttack()
    {
        ShootLaser();

        tm.OneShotTimer(0.1f, () =>
        {
            sm.GetStateContext().attackChangeState = true;
        });
    }

    void EnterVulnerable()
    {
        tm.OneShotTimer(vulnerableTime, () =>
        {
            sm.GetStateContext().vulnerableChangeState = true;
        });
    }

    void EnterDie()
    {
        isDead = true;
        Debug.Log("Boss muerto");
        OnDeath?.Invoke();
    }

    public void ShootLaser()
    {
        if (LaserLightPrefab == null || LaserLightTransform == null || player == null)
            return;

        Vector3 targetPosition = player.position;

        // Evita que apunte al suelo (bloquea eje Y)
        targetPosition.y = LaserLightTransform.position.y;

        Vector3 direction = (targetPosition - LaserLightTransform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject laser = Instantiate( LaserLightPrefab, LaserLightTransform.position, rotation );
        laser.GetComponent<LaserLightBehaviour>().origin = this.transform;
    }

    public void TakeDamage(int damage)
    {
        if (isDead || invulnerable)
            return;

        currentHP -= damage;

        OnTakeDamage?.Invoke();

        if (currentHP <= 0)
        {
            currentHP = 0;
            EnterDie();
        }
    }
}