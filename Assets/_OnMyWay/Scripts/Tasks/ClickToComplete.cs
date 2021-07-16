using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ClickableElement))]
public class ClickToComplete : TaskBehaviour
{
    public UnityEvent activateTaskResponse;

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

        activateTaskResponse?.Invoke();
    }
}
