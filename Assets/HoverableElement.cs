using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoverableElement : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public Action onPointerDownCallback;
    public Action onPointerUpCallback;

    public UnityEvent pointerEnterResponse;
    public UnityEvent pointerExitResponse;

    Vector3 defaultScale;

    public void OnPointerEnter(PointerEventData eventData)
    {
        defaultScale = transform.localScale;
        transform.localScale = defaultScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = defaultScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultScale = transform.localScale;
    }
}
