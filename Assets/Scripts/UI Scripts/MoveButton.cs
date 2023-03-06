using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private SettingsButton settingsButton;

    private bool isDragging;
    private Vector2 pointerOffset;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerPressRaycast.gameObject == gameObject && settingsButton.changeButtonActive)
        {
            isDragging = true;
            pointerOffset = eventData.position - rectTransform.anchoredPosition;
            rectTransform.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging) 
        { 
            rectTransform.anchoredPosition = eventData.position - pointerOffset; 
        }
        else 
            isDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }
}