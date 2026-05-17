using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class FullscreenToggleUI : MonoBehaviour
{
    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        if (_toggle != null)
        {
            // Set initial state based on current screen mode
            _toggle.isOn = Screen.fullScreen;
            
            // Add listener for value changes
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    private void OnToggleValueChanged(bool isFullscreen)
    {
        if (SettingsManager.instance != null)
        {
            SettingsManager.instance.ToggleFullscreen(isFullscreen);
        }
        else
        {
            // Fallback if SettingsManager is not in the scene
            Screen.fullScreen = isFullscreen;
        }
    }

    private void OnDestroy()
    {
        if (_toggle != null)
        {
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }
}
