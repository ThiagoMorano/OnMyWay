using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoverableElement : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public bool callExitOnColliderDisabled = true;

    public Action onPointerEnterCallback;
    public Action onPointerExitCallback;

    public UnityEvent pointerEnterResponse;
    public UnityEvent pointerExitResponse;

    bool _pointerHovering;
    public bool PointerHovering { get { return _pointerHovering; } }

    Collider2D coll;


    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

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

        if (coll.enabled == false) return;
        if (onPointerExitCallback != null)
        {
            onPointerExitCallback();
        }
        pointerExitResponse?.Invoke();
    }
}
