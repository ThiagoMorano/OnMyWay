using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;


public interface IMyInterface {
}

public class LocaleSelector : MonoBehaviour
{
    public string localeCode = "en";
    public Locale locale;
    public bool hoveredState = false;

    [SerializeField]
    public IMyInterface interfaceTest;

    IEnumerator Start()
    {
        hoveredState = false;
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;

        Debug.Log(LocalizationSettings.SelectedLocale.Identifier.Code);

        if(LocalizationSettings.SelectedLocale.Identifier.Code == locale.Identifier.Code) {
            hoveredState = true;
        }
    }

    public void SelectLocale(Locale locale) {
        LocalizationSettings.SelectedLocale = locale;
    }
}
