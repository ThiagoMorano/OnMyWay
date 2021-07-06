using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DraggableItem))]
public class SuitcaseItem : MonoBehaviour
{
    DraggableItem draggableItem;
    Destination _destination;

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
            if (_destination.IsHovering) {
                AddToSuitcase();
            } else {
                ResetToDefaultPosition();
            }
        } else {
            if (_destination.IsHovering) {
                ResetToDefaultPosition();
            } else {
                RemoveFromSuitcase();
            }
        }
    }

    private void AddToSuitcase()
    {
        _isPacked = true;

        var slotPosition = _destination.GetClosestAvailableSlot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
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


    public void SetDestinationReference(Destination destination) {
        _destination = destination;
    }
}
