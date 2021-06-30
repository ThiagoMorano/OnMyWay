using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChangeScaleOnHover : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public float scaleModifier = 1.1f;
    Vector3 defaultScale;

    public void OnPointerEnter(PointerEventData eventData)
    {
        defaultScale = transform.localScale;
        transform.localScale = defaultScale * scaleModifier;
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
