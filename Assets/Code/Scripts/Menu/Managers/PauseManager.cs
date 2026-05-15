using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private InputActionReference _pauseGame;
    [SerializeField] private GameObject _pauseMenu;

    private bool _isPaused = false;

    private void OnEnable()
    {
        if (_pauseGame != null)
        {
            _pauseGame.action.Enable();
            _pauseGame.action.performed += OnPausePerformed;
        }
    }

    private void OnDisable()
    {
        if (_pauseGame != null)
        {
            _pauseGame.action.performed -= OnPausePerformed;
        }
    }

    private void Start() {
        if (_pauseMenu.activeInHierarchy) { 
            _pauseMenu.SetActive(false);
        }
    }
    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        if (_pauseMenu != null) _pauseMenu.SetActive(true);
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (_pauseMenu != null) _pauseMenu.SetActive(false);
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
