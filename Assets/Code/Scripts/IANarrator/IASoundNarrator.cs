using UnityEngine;

public class IASoundNarrator : MonoBehaviour
{
    [Header("Ajustes de Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _narrateSound;
    [Range(0f, 0.5f)]
    [SerializeField] private float _pitchVariation = 0.12f;

    void Awake()
    {
        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
    }

    public void Narrate(char character)
    {
        // Ignorar espacios y puntos
        if (character == ' ' || character == '.') return;

        // Verificar si hay clip y audio source
        if (_narrateSound == null || _audioSource == null) return;

        // Play sound with pitch variation
        _audioSource.pitch = Random.Range(1f - _pitchVariation, 1f + _pitchVariation);
        _audioSource.PlayOneShot(_narrateSound);
    }
}
