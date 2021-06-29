using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDestination : TaskBehaviour
{
    public GameObject destinationPin;
    public List<MapDestination> destinations;

    // Start is called before the first frame update
    void Start()
    {
        destinations = new List<MapDestination>(GetComponentsInChildren<MapDestination>());
        foreach(var destination in destinations) {
            destination.SetTaskReference(this);
            destination.onSelectCallback += this.OnComplete;
        }

        this.onCompleteCallback += DisableTask;
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

    public void SetSelected(MapDestination hoveredStateDestination) {
        // TODO: store the destination string here

        // hoveredStateDestination.hoveredState.SetActive(true);
        // this.SetPinActive(false);
    }

}
