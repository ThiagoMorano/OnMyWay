using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsScreen;
    public GameObject creditsScreen;

    TimelineController timelineController;

    private void Awake() {
        pauseMenu.SetActive(false);
    }

    private void Start()
    {
        FindReferences();
    }

    private void FindReferences()
    {
        timelineController = FindObjectOfType<TimelineController>();
    }


    public void PauseGame()
    {
        gameObject.SetActive(true);
        pauseMenu.SetActive(true);
        settingsScreen.SetActive(true);
        creditsScreen.SetActive(false);
        if (timelineController == null)
        {
            FindReferences();
        }
        timelineController.SetSettingsOpen(true);
        Time.timeScale = 0f;
    }


    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        timelineController.SetSettingsOpen(false);
        Time.timeScale = 1f;
    }
}
