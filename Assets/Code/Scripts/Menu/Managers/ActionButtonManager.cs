using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionButtonManager : MonoBehaviour
{


    public enum BUTTONS
    {
        PLAY,
        OPTIONS,
        CONTINUE,
        MAINMENU,
        CREDITS,
        EXIT
    }
    public BUTTONS currentButton;

    private Button _button;

    private void Awake()
    {
        if (_button == null) _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (_button != null) _button.onClick.AddListener(OnButtonPressed);
    }
    private void OnDisable()
    {
        if (_button != null) _button.onClick.RemoveListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        switch (currentButton)
        {
            case BUTTONS.PLAY:
                if(AudioManager.instance != null) AudioManager.instance.PlayClick();
                SceneManager.LoadScene("Puzzles");
                break;
            case BUTTONS.OPTIONS:
                if (AudioManager.instance != null) AudioManager.instance.PlayClick();
                SceneManager.LoadScene("Options");
                break;
            case BUTTONS.MAINMENU:
                if (AudioManager.instance != null) AudioManager.instance.PlayClick();
                SceneManager.LoadScene("MainMenu");
                break;
            case BUTTONS.CONTINUE:
                if (AudioManager.instance != null) AudioManager.instance.PlayClick();
                break;
            case BUTTONS.CREDITS:
                if (AudioManager.instance != null) AudioManager.instance.PlayClick();
                SceneManager.LoadScene("Credits");
                break;
            case BUTTONS.EXIT:
                #if UNITY_EDITOR
                    if(AudioManager.instance != null) AudioManager.instance.PlayClick();
                    EditorApplication.isPlaying = false; // Detiene el juego en el editor
                #else
                    AudioManager.instance.PlayClick();
                    Application.Quit(); // Cierra la build final
                #endif
                break;
        }
    }
}
