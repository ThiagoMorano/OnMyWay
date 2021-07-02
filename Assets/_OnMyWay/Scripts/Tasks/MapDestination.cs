using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

public class MapDestination : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject hoveredState;
    public LocalizedString noteLocalizationKey;

    bool _wasSelected = false;
    Collider2D _coll;
    SelectDestination _destinationTask;
    ClickableElement clickable;

    public Action onSelectCallback;

    // Start is called before the first frame update
    void Start()
    {
        _coll = GetComponent<Collider2D>();
        // coll.enabled = false;
        if(hoveredState != null) {
            hoveredState.SetActive(false);
        }

        clickable = GetComponent<ClickableElement>();
        clickable.onPointerUpCallback += Select;
    }

    private void Select() {
        _wasSelected = true;
        _destinationTask.SetSelected(this);
        onSelectCallback();
    }

    public void SetTaskItemActive(bool value) {
        _coll.enabled = value;
    }

    public void SetTaskReference(SelectDestination destinationTask) {
        this._destinationTask = destinationTask;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(hoveredState != null) {
            hoveredState?.SetActive(true);
        }
        if(_destinationTask != null) {
            _destinationTask.SetPinActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!_wasSelected) {
            if(hoveredState != null) {
            hoveredState?.SetActive(false);
            }
            if(_destinationTask != null) {
                _destinationTask.SetPinActive(true);
            }
        }
    }
}
