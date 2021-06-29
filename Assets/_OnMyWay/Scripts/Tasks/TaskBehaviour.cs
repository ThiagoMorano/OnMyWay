using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class TaskBehaviour : MonoBehaviour
{
    [Tooltip("Not necessary to be filled. Responses could be added as Action callbacks")]
    public UnityEvent completeResponse;
    public Action onCompleteCallback;

    protected void OnComplete()
    {
        Debug.Log(gameObject.name + " task completed");

        if(onCompleteCallback != null) onCompleteCallback();
        completeResponse?.Invoke();
    }

    public abstract void ActivateTask();
}