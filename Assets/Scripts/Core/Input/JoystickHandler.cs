using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _joystickArea;
    [SerializeField] private Image _joystickBackground;
    [SerializeField] private Image _joystick;

    [SerializeField] private Color _activeJoystickColor = Color.white;
    [SerializeField] private Color _inactiveJoystickColor = Color.gray;

    private Vector2 _direction;

    private Vector3 _startBackgroundPos;
    private bool _isActive;

    public Vector2 Direction => _direction;

    private void Start()
    {
        _startBackgroundPos = _joystickBackground.rectTransform.position;
    }

    private void ClickEffect()
    {
        _isActive = !_isActive;
        
        if (_isActive)
        {
            _joystick.color = _activeJoystickColor;
        }
        else
        {
            _joystick.color = _inactiveJoystickColor;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _joystickBackground.rectTransform,
                eventData.position,
                null,
                out var joystickPosition) == false)
            return;

        joystickPosition.x = joystickPosition.x * 2f /
                             _joystickBackground.rectTransform.rect.width;
        joystickPosition.y = joystickPosition.y * 2f /
                             _joystickBackground.rectTransform.rect.height;

        _direction = new Vector2(joystickPosition.x, joystickPosition.y);

        if (_direction.magnitude > 1f)
            _direction = _direction.normalized;

        _joystick.rectTransform.anchoredPosition =
            new Vector2(
                _direction.x * (_joystickBackground.rectTransform.rect.width / 2f),
                _direction.y * (_joystickBackground.rectTransform.rect.height / 2f)
            );
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ClickEffect();

        _joystickBackground.rectTransform.position = eventData.position;

        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ClickEffect();

        _joystickBackground.rectTransform.position = _startBackgroundPos;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
        _direction = Vector2.zero;
    }
}