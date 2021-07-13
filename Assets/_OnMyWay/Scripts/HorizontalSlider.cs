using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSlider : MonoBehaviour
{
    public GameObject handler;
    public Transform begin, end;

    public AnimationCurve lerpY;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (handler.transform.position.x < begin.position.x)
        {
            handler.transform.position = new Vector3(begin.position.x, handler.transform.position.y, handler.transform.position.z);
        }
        if (handler.transform.position.x > end.position.x)
        {
            handler.transform.position = new Vector3(end.position.x, handler.transform.position.y, handler.transform.position.z);
        }

        handler.transform.position = new Vector3(handler.transform.position.x, EvaluateY(), handler.transform.position.z);
    }

    private float EvaluateY()
    {
        return Mathf.Lerp(
            begin.position.y,
            end.position.y,
            lerpY.Evaluate((handler.transform.position.x - begin.position.x) / end.position.x - begin.position.x)
        );
    }
}
