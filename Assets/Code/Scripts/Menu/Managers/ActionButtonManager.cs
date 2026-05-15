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
                SceneManager.LoadScene("Play");
                break;
            case BUTTONS.OPTIONS:
            case BUTTONS.MAINMENU:
            case BUTTONS.CONTINUE:
                //PauseManager.instance?.SetPause();
                break;
            case BUTTONS.CREDITS:
                //PauseManager.instance?.SetPause();
                break;
            case BUTTONS.EXIT:
                #if UNITY_EDITOR
                    EditorApplication.isPlaying = false; // Detiene el juego en el editor
                #else
                    Application.Quit(); // Cierra la build final
                #endif
                break;
        }
    }
}
