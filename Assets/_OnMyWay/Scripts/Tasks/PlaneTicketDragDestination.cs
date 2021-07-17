using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// @TODO: extract abstract class or interface from this and Destination.
// Destination was supposed to be a generic class to indicate the destination of any task
// involving drag and dropping, but ended up having specific implementations from the
// PackingTask. Decoupling them would leave a generic class that can also be used to 
// refactor this one.
public class PlaneTicketDragDestination : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] public DraggableItem storedItem;

    private bool _isHovering;
    public bool IsHovering { get { return _isHovering; } }

    private bool _isComplete;
    public bool IsComplete { get { return _isComplete; } }

    public Action onAllItemsPlacedCallback;

    void Start()
    {
        storedItem = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
    }


    public void AddItemTo(DraggableItem draggableItem)
    {
        if (!IsSlotAvailable(0))
        {
            Debug.LogWarning("Slot already in use");
            return;
        }

        storedItem = draggableItem;
        _isComplete = true;
        if (onAllItemsPlacedCallback != null)
        {
            onAllItemsPlacedCallback();
        }
    }

    public bool IsSlotAvailable(int index)
    {
        return storedItem == null;
    }
}
