using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonColorChange : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _targetImage;
    [SerializeField] private Color _selectedColor = Color.yellow;
    private Color _normalColor;

    void Awake()
    {
        // If not assigned, try to get Image component on this object
        if (_targetImage == null) _targetImage = GetComponent<Image>();
        
        if (_targetImage != null)
        {
            _normalColor = _targetImage.color;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        Highlight(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Highlight(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Highlight(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Only return to normal if it's not the currently selected object in the EventSystem
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            Highlight(false);
        }
    }

    private void Highlight(bool state)
    {
        if (_targetImage != null)
        {
            _targetImage.color = state ? _selectedColor : _normalColor;
        }
    }
}
