using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
    public GameObject[] leaves;
    public bool force = false;
    public AirplaneOptions forcedValue;

    // Start is called before the first frame update
    void Start()
    {
        int choice = PlayerPrefs.GetInt("planeChoice", (int)AirplaneOptions.budgetDirect);
        if (force)
        {
            choice = (int)forcedValue;
        }
        int score = 0;
        switch (choice)
        {
            case (int)AirplaneOptions.budgetDirect: // direct, economy, budget
                score = 3;
                break;
            case (int)AirplaneOptions.premiumDirect: // direct, economy, standard
                score = 2;
                break;
            case (int)AirplaneOptions.budgetConnection: // connection, business, budget
                score = 1;
                break;
        }

        for (int i = 0; i < leaves.Length; i++)
        {
            leaves[i].SetActive(false);
        }
        for (int i = 0; i < score; i++)
        {
            leaves[i].SetActive(true);
        }
    }
}
