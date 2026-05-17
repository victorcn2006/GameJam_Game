using UnityEngine;
using StateMachine.Runtime;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

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

    [SerializeField] List<Image> heartImages;

    [Header("Player")]
    [SerializeField] Transform player;

    [Header("Timings")]
    [SerializeField] float idleTime = 2f;
    [SerializeField] float vulnerableTime = 1f;

    [Header("Sonido")]
    AudioSource source;
    [SerializeField] AudioClip sfx;

    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem bigExplosion;
    string previousState = "";

    [Header("Canvas Animation")]
    [SerializeField] RectTransform animatedObject;
    [SerializeField] RectTransform targetTransform;
    [SerializeField] float moveDuration = 2f;

    [SerializeField] Volume volumen;
    [SerializeField] float fadeDuration = 1.5f;

    Coroutine fadeCoroutine;

    [SerializeField] AudioClip toBeContinuedSong;

    void Start()
    {
        sm = GetComponent<StateMachineComponent>();
        tm = TimeManager.Instance;
        source = GetComponent<AudioSource>();
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
        currentHP -= damage;
        heartImages[currentHP].enabled = false;
        source.clip = sfx;
        source.Play();
        explosion.Play();
        heartImages[currentHP].transform.GetChild(0).GetComponent<Image>().enabled = false;
        if (currentHP <= 0)
        {
            currentHP = 0;
            EnterDie();
            bigExplosion.Play();
            source.clip = toBeContinuedSong;
            source.Play();
            
            TimeManager.Instance.OneShotTimer(0.1f, () => Time.timeScale = 0);
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeVolumeWeight(1f));
        }
    }

    IEnumerator FadeVolumeWeight(float targetWeight)
    {
        float startWeight = volumen.weight;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            // Funciona aunque Time.timeScale = 0
            elapsed += Time.unscaledDeltaTime;

            volumen.weight = Mathf.Lerp(
                startWeight,
                targetWeight,
                elapsed / fadeDuration
            );
            yield return null;
        }

        volumen.weight = targetWeight;
        StartCoroutine(MoveUIToTarget());
    }

    IEnumerator MoveUIToTarget()
    {
        Vector3 startPos = animatedObject.position;
        Vector3 targetPos = targetTransform.position;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            // Sigue funcionando con timeScale = 0
            elapsed += Time.unscaledDeltaTime;

            float t = elapsed / moveDuration;

            // Suavizado
            t = Mathf.SmoothStep(0f, 1f, t);

            animatedObject.position = Vector3.Lerp(
                startPos,
                targetPos,
                t
            );

            yield return null;
        }

        animatedObject.position = targetPos;

        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale=1;
        SceneManager.LoadScene("Credits");
    }
}