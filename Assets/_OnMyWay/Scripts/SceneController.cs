using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public List<TaskBehaviour> sceneTasks;
    public TimelineController timelineController;
    public Button nextButton;
    public string nextScene;

    int currentTaskIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        nextButton.gameObject.SetActive(false);
        nextButton.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        currentTaskIndex = 0;
        foreach (var task in sceneTasks)
        {
            if (task != null)
            {
                task.onCompleteCallback += TaskCompleted;
            }
            else
            {
                Debug.LogWarning("Null objects in list of tasks.");
            }
        }

        nextButton.onClick.AddListener(() =>
        {
            this.OnNextButtonPressed();
        });

        timelineController.onDirectorFinishedCallback += OnSceneFinished;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ActivateTask()
    {
        sceneTasks[currentTaskIndex].ActivateTask();
        timelineController.SetWaitingForTask(true);
    }


    private void TaskCompleted()
    {
        currentTaskIndex++;
        timelineController.SetWaitingForTask(false);
    }

    public void ActivateNextButton()
    {
        nextButton.gameObject.SetActive(true);
        timelineController.SetNextButtonOpen(true);
    }


    public void OnNextButtonPressed()
    {
        timelineController.SetNextButtonOpen(false);
    }

    private void OnSceneFinished()
    {
        SceneManager.LoadScene(nextScene);
    }
}
