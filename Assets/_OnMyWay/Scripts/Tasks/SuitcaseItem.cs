using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DraggableItem))]
public class SuitcaseItem : MonoBehaviour
{
    DraggableItem draggableItem;
    public Destination destination;

    Vector3 initialPosition;
    bool _isPacked = false;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;

        draggableItem = GetComponent<DraggableItem>();
        draggableItem.onEndDragCallback += OnStopDragging;
    }

    private void OnStopDragging()
    {
        if(!this._isPacked) {
            if (destination.IsHovering) {
                AddToSuitcase();
            } else {
                ResetToDefaultPosition();
            }
        } else {
            if (destination.IsHovering) {
                ResetToDefaultPosition();
            } else {
                RemoveFromSuitcase();
            }
        }
    }

    private void AddToSuitcase()
    {
        _isPacked = true;

        var slotPosition = destination.GetClosestAvailableSlot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        draggableItem.SetDefaultPosition(slotPosition);
        transform.position = slotPosition;
    }

    private void ResetToDefaultPosition()
    {
        draggableItem.ResetToDefaultPosition();
    }


    private void RemoveFromSuitcase()
    {
        _isPacked = false;

        transform.position = initialPosition;
        draggableItem.SetDefaultPosition(initialPosition);
    }
}