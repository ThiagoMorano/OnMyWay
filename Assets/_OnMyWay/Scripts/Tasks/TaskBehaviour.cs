using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class TaskBehaviour : MonoBehaviour
{
    public UnityEvent completeResponse;
    public Action onCompleteCallback;

    protected void OnComplete()
    {
        Debug.Log("Task completed");

        if(onCompleteCallback != null) onCompleteCallback();
        completeResponse?.Invoke();
    }

    public abstract void ActivateTask();
}