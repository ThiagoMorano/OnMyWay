using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenController : MonoBehaviour
{
    public GameObject scorePage;
    public GameObject destinationPage;
    public GameObject planePathPage;
    public GameObject photosPage;
    public GameObject greenTipsPage;


    public string flightTime = "3";

    private InformationButton[] informationButtons;
    private ActiveWithAirplaneChoice[] choiceSensitiveObjects;

    // Start is called before the first frame update
    void Start()
    {
        informationButtons = FindObjectsOfType<InformationButton>(true);

        choiceSensitiveObjects = FindObjectsOfType<ActiveWithAirplaneChoice>(true);
        AirplaneOptions choice = (AirplaneOptions)PlayerPrefs.GetInt("planeChoice", (int)AirplaneOptions.budgetDirect);
        choice = AirplaneOptions.budgetConnection;
        foreach (var obj in choiceSensitiveObjects)
        {
            obj.Initialize(choice);
        }

        if (choice == AirplaneOptions.budgetConnection)
        {
            flightTime = "9";
        }
        else
        {
            flightTime = "3";
        }
    }


    public void CloseAllInformationPanels()
    {
        foreach (InformationButton infoButtons in informationButtons)
        {
            infoButtons.SetPanelActive(false);
        }
    }


    public void OpenScorePage()
    {
        CloseAllPages();
    }

    private void CloseAllPages()
    {
    }
}
