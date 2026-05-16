using UnityEngine;

public class CreditsScroller : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _scrollSpeed = 50f;
    [SerializeField] private float _resetPositionY = -1000f;
    [SerializeField] private float _endPositionY = 2000f;
    [SerializeField] private bool _loop = false;

    private RectTransform _rectTransform;
    private Vector3 _startPosition;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPosition = _rectTransform.anchoredPosition;
    }

    void Update()
    {
        // Move the text upwards
        _rectTransform.anchoredPosition += Vector2.up * _scrollSpeed * Time.deltaTime;

        // Reset if it reaches the end
        if (_rectTransform.anchoredPosition.y > _endPositionY)
        {
            if (_loop)
            {
                _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _resetPositionY);
            }
            else
            {
                // Stop scrolling or you could trigger an event here
                enabled = false;
            }
        }
    }
}
