using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteAfterTime : TaskBehaviour
{
    public float timeUntilCompletion = 5f;
    [SerializeField] float _elapsedTime = 0f;
    bool hasStartedCounting = false;


    public override void ActivateTask()
    {
        hasStartedCounting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStartedCounting)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= timeUntilCompletion)
            {
                OnComplete();
            }
        }
    }

    protected override void OnComplete()
    {
        hasStartedCounting = false;
        this.enabled = false;

        base.OnComplete();
    }

}
