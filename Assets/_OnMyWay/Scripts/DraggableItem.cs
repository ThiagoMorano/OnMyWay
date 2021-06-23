using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public UnityEvent beginDragResponse;
    public UnityEvent endDragResponse;

    public Action onBeginDragCallback;
    public Action onEndDragCallback;

    Vector3 offset;
    Vector3 defaultPosition;

    Collider2D coll;

    void Start() {
        defaultPosition = transform.position;

        coll = GetComponent<Collider2D>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;

        if(onBeginDragCallback != null) onBeginDragCallback();
        beginDragResponse?.Invoke();
    }


    public void OnDrag(PointerEventData eventData)
    {
        coll.enabled = false;

        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition + offset);
        transform.position = position;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        coll.enabled = true;

        if(onEndDragCallback != null) onEndDragCallback();
        endDragResponse?.Invoke();
    }


    public void ResetToDefaultPosition() {
        transform.position = defaultPosition;
    }

    public void SetDefaultPosition(Vector3 newDefaultPosition) {
        defaultPosition = newDefaultPosition;
    }
}
