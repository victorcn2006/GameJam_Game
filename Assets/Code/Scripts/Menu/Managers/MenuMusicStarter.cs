using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicStarter : MonoBehaviour
{
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _puzzlesMusic;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateMusic(scene.name);
    }

    private void Start()
    {
        // Handle the initial scene we are in
        UpdateMusic(SceneManager.GetActiveScene().name);
    }

    private void UpdateMusic(string sceneName)
    {
        if (AudioManager.instance == null) return;

        Debug.Log("Scene Loaded: " + sceneName);
        
        AudioClip targetMusic = null;

        if (sceneName == "Puzzles")
        {
            targetMusic = _puzzlesMusic;
        }
        else
        {
            targetMusic = _menuMusic;
        }

        if (targetMusic != null)
        {
            AudioManager.instance.PlayMusic(targetMusic);
        }
    }
}
