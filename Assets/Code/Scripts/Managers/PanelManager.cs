using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager instance { get; private set; }

    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _pausePanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActiveOptions() { 
        if (_optionsPanel != null) _optionsPanel.SetActive(true);
        if (_pausePanel != null) _pausePanel.SetActive(false);
    }

    public void DesactiveOptions() {
        if (_optionsPanel != null) _optionsPanel.SetActive(false);
        if (_pausePanel != null) _pausePanel.SetActive(true);
    }

    public bool IsOptionsActive()
    {
        return _optionsPanel != null && _optionsPanel.activeInHierarchy;
    }
}
