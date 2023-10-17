using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas parentCanvas;
    private Vector2 dragOffset;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out var localPointerPosition);
        dragOffset = rectTransform.anchoredPosition - localPointerPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out var localPointerPosition))
        {
            rectTransform.anchoredPosition = localPointerPosition + dragOffset;
        }
    }


}
