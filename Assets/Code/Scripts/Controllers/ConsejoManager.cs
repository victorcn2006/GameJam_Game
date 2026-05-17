using System.Collections;
using TMPro;
using UnityEngine;

public class ConsejoManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text textoConsejo;

    [Header("Contenido")]
    [TextArea(2, 5)]
    [SerializeField] private string consejo;

    [Header("Configuración")]
    [SerializeField] private float duracionFade = 1f;
    [SerializeField] private float tiempoVisible = 5f;

    private bool activado = false;

    private void Start()
    {
        // Ocultar al iniciar
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar Player
        if (!activado && other.CompareTag("Player"))
        {
            activado = true;

            // Asignar texto
            if (textoConsejo != null)
            {
                textoConsejo.text = consejo;
            }

            StartCoroutine(MostrarConsejo());
        }
    }

    private IEnumerator MostrarConsejo()
    {
        // Fade In
        float tiempo = 0f;

        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, tiempo / duracionFade);
            yield return null;
        }

        canvasGroup.alpha = 1f;

        // Esperar visible
        yield return new WaitForSeconds(tiempoVisible);

        // Fade Out
        tiempo = 0f;

        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, tiempo / duracionFade);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}