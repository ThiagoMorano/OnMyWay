using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Components;

public class SelectDestination : TaskBehaviour
{
    public GameObject destinationPin;
    public List<MapDestination> destinations;
    public LocalizeStringEvent noteAboutDestination;

    MapDestination _selectedDestination;


    // Start is called before the first frame update
    void Start()
    {
        destinations = new List<MapDestination>(GetComponentsInChildren<MapDestination>());
        foreach (var destination in destinations)
        {
            destination.SetTaskReference(this);
            destination.onSelectCallback += this.OnComplete;
        }

        // this.onCompleteCallback += DisableTask;
    }

    public override void ActivateTask()
    {
        foreach (var destination in destinations)
        {
            destination.SetTaskItemActive(this);
        }
    }

    public void SetPinActive(bool value)
    {
        destinationPin.SetActive(value);
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        DisableTask();
        RefereshDestinationNoteText();
    }

    private void DisableTask()
    {
        foreach (var destination in destinations)
        {
            destination.SetTaskItemActive(false);
        }
    }


    private void RefereshDestinationNoteText()
    {
        noteAboutDestination.StringReference.SetReference(
            _selectedDestination.noteLocalizationKey.TableReference,
            _selectedDestination.noteLocalizationKey.TableEntryReference
        );
        noteAboutDestination.RefreshString();
    }

    public void SetSelected(MapDestination hoveredStateDestination)
    {
        _selectedDestination = hoveredStateDestination;
        DisableTask();

        // TODO: store the destination string here
        // hoveredStateDestination.hoveredState.SetActive(true);
        // this.SetPinActive(false);
    }
}
