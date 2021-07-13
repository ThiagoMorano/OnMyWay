using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;


public class LocaleSelector : MonoBehaviour
{
    public Locale locale;
    public GameObject selectedVisuals;
    public bool selected = false;

    IEnumerator Start()
    {
        selected = false;
        selectedVisuals.SetActive(false);
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;

        if (LocalizationSettings.SelectedLocale.Identifier.Code == locale.Identifier.Code)
        {
            selected = true;
            selectedVisuals.SetActive(true);
        }

        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
    }

    private void OnLocaleChanged(Locale newLocale)
    {
        if (newLocale.Identifier.Code == locale.Identifier.Code)
        {
            selected = true;
            selectedVisuals.SetActive(true);
        }
        else
        {
            selected = false;
            selectedVisuals.SetActive(false);
        }
    }

    public void SelectLocale(Locale locale)
    {
        LocalizationSettings.SelectedLocale = locale;
    }

    // void Update() {
    //     selectedVisuals.SetActive(selected);
    // }
}
