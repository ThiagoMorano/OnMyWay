using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class SetupDestinationPage : MonoBehaviour
{
    [System.Serializable]
    struct SetupDestination
    {
        public DestinationChoice destination;
        public GameObject highlightedObject;
        public LocalizedString noteLocalizationKey;
    };


    [SerializeField] SetupDestination[] directives;

    public LocalizeStringEvent noteAboutDestination;



    // public bool force;
    // public DestinationChoice forcedValue;

    // Start is called before the first frame update
    void Start()
    {
        DestinationChoice choice = (DestinationChoice)PlayerPrefs.GetInt("destinationChoice", (int)DestinationChoice.Madrid);
        // if (force)
        // {
        //     choice = forcedValue;
        // }
        for (int i = 0; i < directives.Length; i++)
        {
            directives[i].highlightedObject.SetActive(false);
            if (directives[i].destination == choice)
            {
                SetDestination(directives[i]);
            }
        }
    }

    private void SetDestination(SetupDestination directive)
    {
        directive.highlightedObject.SetActive(true);
        SetDestinationNote(directive.noteLocalizationKey);
    }

    private void SetDestinationNote(LocalizedString localizedString)
    {
        noteAboutDestination.StringReference.SetReference(
            localizedString.TableReference,
            localizedString.TableEntryReference
        );
        noteAboutDestination.RefreshString();
    }
}
