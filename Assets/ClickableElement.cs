using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickableElement : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent response;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(name + " clicked.");
        response?.Invoke();
    }
}
