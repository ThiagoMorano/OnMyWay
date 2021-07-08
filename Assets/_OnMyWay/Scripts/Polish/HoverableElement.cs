using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoverableElement : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public Action onPointerEnterCallback;
    public Action onPointerExitCallback;

    public UnityEvent pointerEnterResponse;
    public UnityEvent pointerExitResponse;

    bool _pointerHovering;
    public bool PointerHovering { get { return _pointerHovering; } }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _pointerHovering = true;
        if (onPointerEnterCallback != null)
        {
            onPointerEnterCallback();
        }
        pointerEnterResponse?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pointerHovering = false;
        if (onPointerExitCallback != null)
        {
            onPointerExitCallback();
        }
        pointerExitResponse?.Invoke();
    }
}
