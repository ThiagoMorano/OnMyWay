using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter2Controller : MonoBehaviour
{
    public List<TaskBehaviour> sceneTask;
    public TimelineController timelineController;
    public Button nextButton;

    public GameObject nextEncounter;

    int currentTask = 0;

    // Start is called before the first frame update
    void Start()
    {
        nextButton.gameObject.SetActive(false);
        nextButton.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        foreach(var task in sceneTask) {
            task.onCompleteCallback += TaskCompleted;
        }

        nextButton.onClick.AddListener(() => {
            this.OnNextButtonPressed();
        });

        timelineController.onDirectorFinishedCallback += OnSceneFinished;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ActivateTask() {
        sceneTask[0].ActivateTask();
    }


    private void TaskCompleted() {
        currentTask++;
        timelineController.ResumeTimeline();
    }

    public void ActivateNextButton() {
        nextButton.gameObject.SetActive(true);
    }


    public void OnNextButtonPressed() {
        timelineController.ResumeTimeline();
    }

    private void OnSceneFinished() {
        nextEncounter.SetActive(true);
    }
}
