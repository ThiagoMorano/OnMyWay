using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Destination : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public List<Transform> slots;
    [SerializeField] private bool[] _slotsInUse;
    [SerializeField] private SuitcaseItem[] _itemsStored;

    private bool _isHovering;
    public bool IsHovering { get { return _isHovering; } }

    void Start()
    {
        _slotsInUse = new bool[slots.Count];
        _itemsStored = new SuitcaseItem[slots.Count];
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
        _itemsStored[slotIndex] = suitcaseItem;
    }

    public void RemoveItemFromSlot(int slotIndex)
    {
        _slotsInUse[slotIndex] = false;
        _itemsStored[slotIndex] = null;
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
