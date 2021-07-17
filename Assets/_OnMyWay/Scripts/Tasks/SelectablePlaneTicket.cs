using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(HoverableElement))]
[RequireComponent(typeof(DraggableItem))]
public class SelectablePlaneTicket : MonoBehaviour
{
    public AirplaneOptions airplaneOption;
    public Transform transformOnHover;

    private Vector3 _defaultPosition;
    private Vector3 _defaultRotation;
    private Vector3 _defaultScale;

    private bool selected;

    private Collider2D _collider2D;
    private HoverableElement _hoverableElement;
    private DraggableItem _draggableItem;

    private PlaneTicketDragDestination _dragDestination;
    private PlaneTicketSelectionTask _ticketSelectionTask;

    // Start is called before the first frame update
    void Start()
    {
        _defaultPosition = transform.localPosition;
        _defaultRotation = transform.localEulerAngles;
        _defaultScale = transform.localScale;

        _collider2D = GetComponent<Collider2D>();

        _hoverableElement = GetComponent<HoverableElement>();
        _hoverableElement.onPointerEnterCallback += OnStartHover;
        _hoverableElement.onPointerExitCallback += OnEndHover;

        _draggableItem = GetComponent<DraggableItem>();
        _draggableItem.onEndDragCallback += OnStopDragging;

        _dragDestination = FindObjectOfType<PlaneTicketDragDestination>(true);
        _ticketSelectionTask = FindObjectOfType<PlaneTicketSelectionTask>(true);
        _ticketSelectionTask.onCompleteCallback += DisableTicket;

        Debug.Log("Complete ticket start");
    }

    private void OnStartHover()
    {
        if (selected) return;
        transform.localPosition = transformOnHover.localPosition;
        transform.localEulerAngles = transformOnHover.localEulerAngles;
        transform.localScale = transformOnHover.localScale;
    }

    private void OnEndHover()
    {
        if (selected) return;
        ResetTicketToDefault();
    }

    private void ResetTicketToDefault()
    {
        Debug.Log("resetting to default");
        transform.localPosition = _defaultPosition;
        transform.localEulerAngles = _defaultRotation;
        transform.localScale = _defaultScale;
    }


    private void OnStopDragging()
    {
        if (_dragDestination.IsHovering)
        {
            AddToDestination();
        }
        else
        {
            ResetTicketToDefault();
        }
    }

    private void AddToDestination()
    {
        _dragDestination.AddItemTo(this._draggableItem);
        this.OnSelect();
    }

    private void OnSelect()
    {
        selected = true;
        transform.position = _dragDestination.transform.position;
        transform.rotation = _dragDestination.transform.rotation;

        _collider2D.enabled = false;

        _ticketSelectionTask.OnSelectTicket(this);
    }


    private void DisableTicket()
    {
        this.enabled = false;
        _collider2D.enabled = false;
        _draggableItem.enabled = false;
    }
}
