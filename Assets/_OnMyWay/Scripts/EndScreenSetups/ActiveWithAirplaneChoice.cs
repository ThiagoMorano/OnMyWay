using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWithAirplaneChoice : MonoBehaviour
{
    public AirplaneOptions[] airplaneChoices;

    private bool _hasInit = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!_hasInit)
        {
            AirplaneOptions choice = (AirplaneOptions)PlayerPrefs.GetInt("planeChoice", (int)AirplaneOptions.budgetDirect);
            Initialize((AirplaneOptions)choice);
        }
    }

    public void Initialize(AirplaneOptions chosenOption)
    {
        _hasInit = true;

        bool activeValue = false;
        for (int i = 0; i < airplaneChoices.Length; i++)
        {
            if (chosenOption == airplaneChoices[i])
            {
                activeValue = true;
            }
        }
        gameObject.SetActive(activeValue);
    }
}
