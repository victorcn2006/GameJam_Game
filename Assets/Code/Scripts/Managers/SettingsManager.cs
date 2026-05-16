using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance { get; private set; }

    private const string FULLSCREEN_KEY = "IsFullscreen";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        
        // Save to PlayerPrefs (1 for true, 0 for false)
        PlayerPrefs.SetInt(FULLSCREEN_KEY, isFullscreen ? 1 : 0);
        PlayerPrefs.Save();

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayClick();
        }
    }

    private void LoadSettings()
    {
        // Default to fullscreen (1) if no key exists
        bool isFullscreen = PlayerPrefs.GetInt(FULLSCREEN_KEY, 1) == 1;
        Screen.fullScreen = isFullscreen;
    }
}
