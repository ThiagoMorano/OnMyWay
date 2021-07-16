using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AirplaneOptions
{
    budgetDirect = 0,
    premiumDirect = 1,
    budgetConnection = 2
}

public class AirplaneSetup : MonoBehaviour
{
    public GameObject budgetDirect;
    public GameObject premiumDirect;
    public GameObject budgetConnection;

    public AirplaneOptions defaultOption;


    public SpriteRenderer airplaneSticker;
    public Sprite budgetAirplaneSprite;
    public Sprite premiumAirplaneSprite;

    [HideInInspector] public string flightTime = "3";


    void Awake()
    {
        int planeChoice = PlayerPrefs.GetInt("planeChoice", (int)defaultOption);
        SetupScene(planeChoice);
    }

    private void SetupScene(int planeChoice)
    {
        switch (planeChoice)
        {
            case (int)AirplaneOptions.budgetDirect:
                budgetDirect.SetActive(true);
                premiumDirect.SetActive(false);
                budgetConnection.SetActive(false);

                airplaneSticker.sprite = budgetAirplaneSprite;
                flightTime = "3";
                break;
            case (int)AirplaneOptions.premiumDirect:
                budgetDirect.SetActive(false);
                premiumDirect.SetActive(true);
                budgetConnection.SetActive(false);

                airplaneSticker.sprite = budgetAirplaneSprite;
                flightTime = "3";
                break;
            case (int)AirplaneOptions.budgetConnection:
                budgetDirect.SetActive(false);
                premiumDirect.SetActive(false);
                budgetConnection.SetActive(true);

                airplaneSticker.sprite = budgetAirplaneSprite;
                flightTime = "9";
                break;
        }
    }
}
