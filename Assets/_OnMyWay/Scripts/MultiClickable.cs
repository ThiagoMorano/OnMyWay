using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MultiClickable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    public Action onPointerDownCallback;
    public Action onPointerUpCallback;

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
        Debug.Log("Pointer Down");
        if(onPointerDownCallback != null) {
            onPointerDownCallback();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(_pointerHovering) {
            Debug.Log("Pointer Up");
            if(onPointerUpCallback != null) {
                onPointerUpCallback();
            }
        }
    }
}
