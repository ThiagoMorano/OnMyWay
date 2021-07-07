using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertObjectNearRightEdge : MonoBehaviour
{
    public Transform anchor;
    public Transform objectToInvert;
    public float threshold = 0.8f;

    private Vector3 defaultValues;

    void OnEnable()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(anchor.position);
        if ((screenPoint.x / Screen.width) > threshold)
        {
            objectToInvert.localScale = new Vector3(-1, objectToInvert.localScale.y, objectToInvert.localScale.z);
        }
        else
        {
            objectToInvert.localScale = new Vector3(1, objectToInvert.localScale.y, objectToInvert.localScale.z);
        }
    }
}
