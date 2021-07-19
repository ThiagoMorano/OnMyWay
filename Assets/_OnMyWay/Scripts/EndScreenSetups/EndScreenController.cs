using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenController : MonoBehaviour
{
    public List<GameObject> endScreenPages;
    public int currentPage = 0;

    [HideInInspector] public string flightTime = "3";

    private InformationButton[] informationButtons;
    private ActiveWithAirplaneChoice[] choiceSensitiveObjects;


    public ClickableElement previousButton;
    public ClickableElement nextButton;

    public GameEvent movePagesSFXEvent;

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

        if (previousButton)
        {
            previousButton.onPointerUpCallback += PreviousPage;
        }
        if (nextButton)
        {
            nextButton.onPointerUpCallback += NextPage;
        }


        currentPage = 0;
        OpenPage(endScreenPages[currentPage]);
    }

    public void CloseAllInformationPanels()
    {
        foreach (InformationButton infoButtons in informationButtons)
        {
            infoButtons.SetPanelActive(false);
        }
    }


    private void NextPage()
    {
        ClosePage(endScreenPages[currentPage]);
        currentPage++;
        if (currentPage >= endScreenPages.Count) currentPage = endScreenPages.Count - 1;
        OpenPage(endScreenPages[currentPage]);
    }

    private void PreviousPage()
    {
        ClosePage(endScreenPages[currentPage]);
        currentPage--;
        if (currentPage < 0) currentPage = 0;
        OpenPage(endScreenPages[currentPage]);
    }

    private void ClosePage(GameObject pageObject)
    {
        movePagesSFXEvent?.Raise();
        pageObject.SetActive(false);
    }

    private void OpenPage(GameObject pageObject)
    {
        // CloseAllPages();
        pageObject?.SetActive(true);
    }


    public void OpenScorePage()
    {
        ClosePage(endScreenPages[currentPage]);
        currentPage = 0;
        OpenPage(endScreenPages[currentPage]);
    }

    public void OpenDestinationPage()
    {
        ClosePage(endScreenPages[currentPage]);
        currentPage = 1;
        OpenPage(endScreenPages[currentPage]);
    }

    public void OpenTaskPage()
    {
        ClosePage(endScreenPages[currentPage]);
        currentPage = 2;
        OpenPage(endScreenPages[currentPage]);
    }

    public void OpenPhotoPage()
    {
        ClosePage(endScreenPages[currentPage]);
        currentPage = 6;
        OpenPage(endScreenPages[currentPage]);
    }

    public void OpenGreenTipPage()
    {
        ClosePage(endScreenPages[currentPage]);
        currentPage = 8;
        OpenPage(endScreenPages[currentPage]);
    }

    private void CloseAllPages()
    {
        foreach (var page in endScreenPages)
        {

        }
    }


    void Update()
    {
        previousButton?.gameObject.SetActive(currentPage != 0);
        nextButton?.gameObject.SetActive(currentPage != (endScreenPages.Count - 1));
    }
}
