using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("UI Sound Clips")]
    [SerializeField] private AudioClip _clickClip;
    [SerializeField] private AudioClip _navigationClip;
    [SerializeField] private AudioClip _mainMenuMusic;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
            // Ensure we have the sources if not assigned
            if (_musicSource == null) _musicSource = gameObject.AddComponent<AudioSource>();
            if (_sfxSource == null) _sfxSource = gameObject.AddComponent<AudioSource>();

            _musicSource.loop = true;
        } 
        else Destroy(this.gameObject);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        if (_musicSource.clip == clip) return;

        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        _sfxSource.PlayOneShot(clip);
    }

    public void PlayClick() 
    { 
        if (_clickClip != null) _sfxSource.PlayOneShot(_clickClip); 
    }

    public void PlayNavigation() 
    { 
        if (_navigationClip != null) _sfxSource.PlayOneShot(_navigationClip); 
    }
}
