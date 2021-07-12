using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ClickableElement))]
public class ClickToComplete : TaskBehaviour
{
    Collider2D _coll;
    ClickableElement _clickable;

    // Start is called before the first frame update
    void Start()
    {
        _coll = GetComponent<Collider2D>();
        _coll.enabled = false;

        _clickable = GetComponent<ClickableElement>();
        _clickable.onPointerUpCallback += OnComplete;
    }

    public override void ActivateTask()
    {
        _coll.enabled = true;
    }
}
