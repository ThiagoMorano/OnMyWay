using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public enum DestinationChoice
{
    Madrid,
    Paris,
    London,
    Warsaw
}


public class AirportSetup : MonoBehaviour
{
    public DestinationChoice defaultChoice;

    [Space(20)]

    public AirportSetupObjects objectsToBeSet;
    public AirportSetupValues madrid;
    public AirportSetupValues paris;
    public AirportSetupValues london;
    public AirportSetupValues warsaw;


    // Start is called before the first frame update
    void Awake()
    {
        int destinationChoice = PlayerPrefs.GetInt("destinationChoice", (int)defaultChoice);
        switch (destinationChoice)
        {
            case (int)DestinationChoice.Madrid:
                SetupObjects(madrid);
                break;
            case (int)DestinationChoice.Paris:
                SetupObjects(paris);
                break;
            case (int)DestinationChoice.London:
                SetupObjects(london);
                break;
            case (int)DestinationChoice.Warsaw:
                SetupObjects(warsaw);
                break;
        }
    }

    private void SetupObjects(AirportSetupValues setupValues)
    {
        objectsToBeSet.airportScreen.StringReference.SetReference(
            setupValues.screenString.TableReference,
            setupValues.screenString.TableEntryReference
        );
        objectsToBeSet.airportScreen.RefreshString();

        // ---
        objectsToBeSet.budgetDirectBigArrival.StringReference.SetReference(
            setupValues.ticketFullString.TableReference,
            setupValues.ticketFullString.TableEntryReference
        );
        objectsToBeSet.budgetDirectBigArrival.RefreshString();

        objectsToBeSet.budgetDirectSmallArrival.StringReference.SetReference(
            setupValues.ticketAirportCode.TableReference,
            setupValues.ticketAirportCode.TableEntryReference
        );
        objectsToBeSet.budgetDirectSmallArrival.RefreshString();

        // ---
        objectsToBeSet.premiumDirectBigArrival.StringReference.SetReference(
            setupValues.premiumTicketString.TableReference,
            setupValues.premiumTicketString.TableEntryReference
        );
        objectsToBeSet.premiumDirectBigArrival.RefreshString();

        objectsToBeSet.premiumDirectSmallArrival.StringReference.SetReference(
            setupValues.ticketAirportCode.TableReference,
            setupValues.ticketAirportCode.TableEntryReference
        );
        objectsToBeSet.premiumDirectSmallArrival.RefreshString();

        // ---
        objectsToBeSet.budgetConnectionBigArrival.StringReference.SetReference(
            setupValues.ticketFullString.TableReference,
            setupValues.ticketFullString.TableEntryReference
        );
        objectsToBeSet.budgetConnectionBigArrival.RefreshString();

        objectsToBeSet.budgetConnectionSmallArrival.StringReference.SetReference(
            setupValues.ticketAirportCode.TableReference,
            setupValues.ticketAirportCode.TableEntryReference
        );
        objectsToBeSet.budgetConnectionSmallArrival.RefreshString();
    }


    [System.Serializable]
    public struct AirportSetupObjects
    {
        public LocalizeStringEvent airportScreen;
        public LocalizeStringEvent budgetDirectBigArrival;
        public LocalizeStringEvent budgetDirectSmallArrival;
        public LocalizeStringEvent premiumDirectBigArrival;
        public LocalizeStringEvent premiumDirectSmallArrival;
        public LocalizeStringEvent budgetConnectionBigArrival;
        public LocalizeStringEvent budgetConnectionSmallArrival;
    }

    [System.Serializable]
    public struct AirportSetupValues
    {
        public LocalizedString screenString;
        public LocalizedString ticketFullString;
        public LocalizedString ticketAirportCode;
        public LocalizedString premiumTicketString;
    }
}
