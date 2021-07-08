using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DraggableItem))]
public class SuitcaseItem : MonoBehaviour
{
    public enum PackCathegory
    {
        BAD,
        GOOD,
        NORMAL
    }

    DraggableItem draggableItem;
    Destination _destination;

    Vector3 _initialPosition;
    [SerializeField] bool _isPacked = false;
    [SerializeField] int _packedInSlot = -1;

    public PackCathegory cathegory;

    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;

        draggableItem = GetComponent<DraggableItem>();
        draggableItem.onEndDragCallback += OnStopDragging;
    }

    private void OnStopDragging()
    {
        if (this._isPacked)
        {
            if (_destination.IsHovering)
            {
                RemoveFromSuitcase();
                if (_destination.HasAvailableSlot())
                {
                    AddToSuitcase();
                }
                else
                {
                    SetNewDefaultPosition(_initialPosition);
                    ResetToDefaultPosition();
                }
            }
            else
            {
                ResetItem();
            }
        }
        else
        {
            if (_destination.IsHovering && _destination.HasAvailableSlot())
            {
                AddToSuitcase();
            }
            else
            {
                ResetToDefaultPosition();
            }
        }
    }

    private void RemoveFromSuitcase()
    {
        _destination.RemoveItemFromSlot(_packedInSlot);
        _packedInSlot = -1;
        _isPacked = false;
    }


    private void AddToSuitcase()
    {
        _isPacked = true;

        var slotIndex = _destination.GetClosestAvailableSlot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        var slotPosition = _destination.GetPositionOfSlot(slotIndex);
        _destination.AddItemTo(this, slotIndex);
        _packedInSlot = slotIndex;

        SetNewDefaultPosition(slotPosition);
        transform.position = slotPosition;
    }

    private void SetNewDefaultPosition(Vector3 position)
    {
        draggableItem.SetDefaultPosition(position);
    }

    private void ResetToDefaultPosition()
    {
        draggableItem.ResetToDefaultPosition();
    }

    public void ResetItem()
    {
        RemoveFromSuitcase();
        SetNewDefaultPosition(_initialPosition);
        ResetToDefaultPosition();
    }


    public void SetDestinationReference(Destination destination)
    {
        _destination = destination;
    }
}
