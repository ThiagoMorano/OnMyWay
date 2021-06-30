using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ClickableElement))]
public class ClickToComplete : TaskBehaviour
{
    Collider2D coll;
    ClickableElement clickable;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        coll.enabled = false;

        clickable = GetComponent<ClickableElement>();
        clickable.onPointerUpCallback += OnComplete;
    }

    public override void ActivateTask()
    {
        coll.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
