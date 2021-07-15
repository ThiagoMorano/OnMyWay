using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPlane : TaskBehaviour
{
    public DraggableItem handler;
    public Transform begin, end;

    public AnimationCurve lerpY;

    float _evaluatedX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _evaluatedX = EvaluateX();

        if (handler.transform.position.x < begin.position.x)
        {
            handler.transform.position = new Vector3(begin.position.x, handler.transform.position.y, handler.transform.position.z);
        }
        if (handler.transform.position.x > end.position.x)
        {
            handler.transform.position = new Vector3(end.position.x, handler.transform.position.y, handler.transform.position.z);
        }

        handler.transform.position = new Vector3(handler.transform.position.x, EvaluateY(_evaluatedX), handler.transform.position.z);


        handler.transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(32f, -32f, _evaluatedX));
    }

    public float EvaluateX()
    {
        return Mathf.Clamp(
            (handler.transform.position.x - begin.position.x) / (end.position.x - begin.position.x),
            0f,
            1f
        );
    }

    private float EvaluateY(float evaluatedX)
    {
        return Mathf.Lerp(
            begin.position.y,
            end.position.y,
            lerpY.Evaluate(evaluatedX)
        );
    }


    public void AttemptCompletion()
    {
        if (EvaluateX() > 0.97f)
        {
            OnComplete();
        }
    }

    protected override void OnComplete()
    {
        base.OnComplete();

        handler.GetComponent<Collider2D>().enabled = false;
    }

    public override void ActivateTask()
    {
        handler.GetComponent<Collider2D>().enabled = true;
    }
}
