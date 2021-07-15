using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTransformStates : MonoBehaviour
{
    public Vector3 positionOff;
    public Vector3 positionOn;
    public bool currentState;
    public bool local = true;


    // Start is called before the first frame update
    void Start()
    {
        if (local)
        {
            transform.localPosition = currentState ? positionOn : positionOff;
        }
        else
        {
            transform.position = currentState ? positionOn : positionOff;
        }
    }


    public void ToggleState()
    {
        currentState = !currentState;
        if (local)
        {
            transform.localPosition = currentState ? positionOn : positionOff;
        }
        else
        {
            transform.position = currentState ? positionOn : positionOff;
        }
    }
}
