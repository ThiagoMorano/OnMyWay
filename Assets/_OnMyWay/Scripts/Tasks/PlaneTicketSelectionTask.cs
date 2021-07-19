using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTicketSelectionTask : TaskBehaviour
{
    PlaneTicketDragDestination planeTicketDragDestination;

    // Start is called before the first frame update
    void Start()
    {
        planeTicketDragDestination = FindObjectOfType<PlaneTicketDragDestination>();
    }


    public override void ActivateTask()
    {
    }

    public void OnSelectTicket(SelectablePlaneTicket selectablePlaneTicket)
    {
        // Store on player prefs
        PlayerPrefs.SetInt("planeChoice", (int)selectablePlaneTicket.airplaneOption);
        OnComplete();
    }
}
