using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleDraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool IsDragging { get; private set; } = false;
    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDragging = true;
        //print("On Drag Begin" + IsDragging);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        IsDragging = false;
        //print("On Drag End" + IsDragging);
    }

}

