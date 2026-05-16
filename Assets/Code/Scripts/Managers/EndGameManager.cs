using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _finalPanel;
    private void OnEnable() {
        //Boss.Death.AddListener(FinalEvent)
    }
    private void OnDisable() {
        //Boss.Death.RemoveListener(FinalEvent);
    }

    private void Start() {
        if(_finalPanel.activeInHierarchy) _finalPanel.SetActive(false);
    }

    private void FinalEvent() {
        _finalPanel.SetActive(true);
    }
}
