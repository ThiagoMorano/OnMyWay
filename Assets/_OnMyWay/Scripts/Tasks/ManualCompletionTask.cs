using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualCompletionTask : TaskBehaviour
{
    public void CompleteTaskExternally()
    {
        this.OnComplete();
    }

    public override void ActivateTask() {; }
}
