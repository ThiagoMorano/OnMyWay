using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Localization.Components;

public class SelectDestination : TaskBehaviour
{
    public GameObject destinationPin;
    public List<MapDestination> destinations;
    public LocalizeStringEvent noteAboutDestination;

    MapDestination selectedDestination;

    // Start is called before the first frame update
    void Start()
    {
        destinations = new List<MapDestination>(GetComponentsInChildren<MapDestination>());
        foreach(var destination in destinations) {
            destination.SetTaskReference(this);
            destination.onSelectCallback += this.OnComplete;
        }

        // this.onCompleteCallback += DisableTask;
    }

    public override void ActivateTask()
    {
        foreach(var destination in destinations) {
            destination.SetTaskItemActive(this);
        }
    }

    private void DisableTask() {
        foreach(var destination in destinations) {
            destination.SetTaskItemActive(false);
        }
    }

    public void SetPinActive(bool value) {
        destinationPin.SetActive(value);
    }

    protected override void OnComplete() {
        DisableTask();
        // RefereshDestinationNoteText();
        base.OnComplete();
    }

    private void RefereshDestinationNoteText()
    {
        Debug.Log("RefreshDestinationNote");
        noteAboutDestination.StringReference = selectedDestination.noteLocalizationKey;
        noteAboutDestination.RefreshString();
    }

    public void SetSelected(MapDestination hoveredStateDestination) {
        // TODO: store the destination string here
        selectedDestination = hoveredStateDestination;

        // hoveredStateDestination.hoveredState.SetActive(true);
        // this.SetPinActive(false);
    }

}
