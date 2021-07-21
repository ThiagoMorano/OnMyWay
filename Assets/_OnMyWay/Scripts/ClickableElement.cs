using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickableElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    public Action onPointerDownCallback;
    public Action onPointerUpCallback;

    public UnityEvent pointerDownResponse;
    public UnityEvent pointerUpResponse;

    bool _pointerHovering;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _pointerHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pointerHovering = false;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (onPointerDownCallback != null)
        {
            onPointerDownCallback();
        }
        pointerDownResponse?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_pointerHovering)
        {
            if (onPointerUpCallback != null)
            {
                onPointerUpCallback();
            }
            pointerUpResponse?.Invoke();
        }
    }
}
