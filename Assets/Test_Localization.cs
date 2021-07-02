using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Components;


public class Test_Localization : MonoBehaviour
{
    LocalizeStringEvent localizeStringEvent;


    // Start is called before the first frame update
    void Start()
    {
        localizeStringEvent = GetComponent<LocalizeStringEvent>();
        Debug.Log(localizeStringEvent.StringReference);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
