using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultipleTasks : TaskBehaviour
{
    public UnityEvent onActivateResponse;
    [Space(40)]
    public List<TaskBehaviour> subtasks;

    private Dictionary<TaskBehaviour, bool> completionList;


    // Start is called before the first frame update
    void Start()
    {
        completionList = new Dictionary<TaskBehaviour, bool>();

        foreach (var task in subtasks)
        {
            task.onCompleteCallback += () =>
            {
                this.CompleteSubtask(task);
            };
            completionList.Add(task, false);
        }
    }

    private void CompleteSubtask(TaskBehaviour completedTask)
    {
        Debug.Log("Completing subtask " + completedTask);
        if (!completionList.ContainsKey(completedTask)) return;

        completionList[completedTask] = true;
        if (GetOverallCompletion())
        {
            this.OnComplete();
        }
    }

    private bool GetOverallCompletion()
    {
        foreach (var state in completionList.Values)
        {
            if (state == false)
            {
                return false;
            }
        }
        return true;
    }


    public override void ActivateTask()
    {
        Debug.Log("Activate Task " + gameObject.name);
        onActivateResponse?.Invoke();
    }
}
