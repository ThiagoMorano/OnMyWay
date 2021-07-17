using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// @TODO: extract abstract class or interface from this and PlaneTicketDragDestination.
// Destination was supposed to be a generic class to indicate the destinatio of any task
// involving drag and dropping, but ended up having specific implementations from the
// PackingTask. Decoupling them would leave a generic class that can also be used to 
// refactor PlaneTicketSelectionTask.
public class Destination : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public List<Transform> slots;
    [SerializeField] private bool[] _slotsInUse;
    [SerializeField] public SuitcaseItem[] itemsStored;

    private bool _isHovering;
    public bool IsHovering { get { return _isHovering; } }

    private int _itemCounter;
    private bool _isComplete;
    public bool IsComplete { get { return _isComplete; } }

    public Action onAllItemsPlacedCallback;

    void Start()
    {
        _slotsInUse = new bool[slots.Count];
        itemsStored = new SuitcaseItem[slots.Count];
        _itemCounter = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
    }


    public int GetClosestAvailableSlot(Vector3 pos)
    {
        float minDistance = float.MaxValue;
        float sqrDistance;
        int index = -1;
        for (int i = 0; i < slots.Count; i++)
        {
            if (IsSlotAvailable(i))
            {
                sqrDistance = (slots[i].position - pos).sqrMagnitude;
                if (sqrDistance < minDistance)
                {
                    minDistance = sqrDistance;
                    index = i;
                }
            }
        }
        return index;
    }

    public bool IsSlotAvailable(int index)
    {
        if (index < 0 || index >= _slotsInUse.Length) return false;
        return (!_slotsInUse[index]);
    }


    public void AddItemTo(SuitcaseItem suitcaseItem, int slotIndex)
    {
        if (!IsSlotAvailable(slotIndex))
        {
            Debug.LogWarning(String.Format("Slot {0} already in use", slotIndex));
            return;
        }

        _slotsInUse[slotIndex] = true;
        itemsStored[slotIndex] = suitcaseItem;
        _itemCounter++;
        if (_itemCounter == slots.Count)
        {
            _isComplete = true;
            if (onAllItemsPlacedCallback != null)
            {
                onAllItemsPlacedCallback();
            }
        }
    }

    public void RemoveItemFromSlot(int slotIndex)
    {
        _slotsInUse[slotIndex] = false;
        itemsStored[slotIndex] = null;
        _itemCounter--;
        _isComplete = false;
    }


    public void ResetAll()
    {
        for (int i = 0; i < itemsStored.Length; i++)
        {
            if(_slotsInUse[i]) {
                itemsStored[i].ResetItem();
            }
        }
    }


    public bool HasAvailableSlot()
    {
        for (int i = 0; i < _slotsInUse.Length; i++)
        {
            if (_slotsInUse[i] == false)
            {
                return true;
            }
        }
        return false;
    }


    public Vector3 GetPositionOfSlot(int index)
    {
        return slots[index].position;
    }
}
