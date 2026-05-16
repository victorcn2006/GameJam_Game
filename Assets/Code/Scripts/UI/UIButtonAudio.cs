using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class UIButtonAudio : MonoBehaviour, IPointerEnterHandler
{
    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayClickSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            AudioManager.instance.PlayNavigation();
        }
    }

    private void PlayClickSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayClick();
        }
    }
}
