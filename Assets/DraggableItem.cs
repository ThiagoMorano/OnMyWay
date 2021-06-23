using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public UnityEvent beginDragResponse;
    public UnityEvent endDragResponse;

    Vector3 offset;
    Vector3 initialPosition;

    void Start() {
        initialPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition + offset);
        transform.position = position;
    }
}
