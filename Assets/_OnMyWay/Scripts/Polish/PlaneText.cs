using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlaneText : MonoBehaviour
{
    DragPlane dragController;
    TextMeshPro textMeshPro;

    public float min;
    public float max;

    // Start is called before the first frame update
    void Start()
    {
        dragController = FindObjectOfType<DragPlane>();
        textMeshPro = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.alpha = GetTransparencyValue(dragController.EvaluateX());
    }

    private float GetTransparencyValue(float trajectoryPercentage)
    {
        return (trajectoryPercentage - min) / (max - min);
    }
}
