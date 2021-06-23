using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Destination : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public List<Transform> slots;
    [SerializeField]
    private bool _isHovering;
    public bool IsHovering { get {return _isHovering; } }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
    }


    public Vector3 GetClosestAvailableSlot(Vector3 pos) {
        float minDistance = (slots[0].position - pos).sqrMagnitude;
        float sqrDistance;
        int index = 0;
        for (int i = 1; i < slots.Count; i++) {
            sqrDistance = (slots[i].position - pos).sqrMagnitude;
            if(sqrDistance < minDistance) {
                minDistance = sqrDistance;
                index = i;
            }
        }
        return slots[index].position;
    }
}
