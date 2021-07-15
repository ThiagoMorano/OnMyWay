using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingClock : MonoBehaviour
{
    private DragPlane dragController;
    public Transform hoursArm;
    public Transform minutesArm;

    private float _initialHoursRotation;
    private float _initialMinutesRotation;

    [Tooltip("In hours")]
    public float durationDirect = 3f;
    [Tooltip("In hours")]
    public float durationConnection = 9f;

    private float _duration;

    // Start is called before the first frame update
    void Start()
    {
        dragController = FindObjectOfType<DragPlane>();

        int planeChoice = PlayerPrefs.GetInt("planeChoice", (int)AirplaneOptions.budgetConnection);
        if (planeChoice == (int)AirplaneOptions.budgetConnection)
        {
            _duration = durationConnection;
        }
        else
        {
            _duration = durationDirect;
        }

        _initialHoursRotation = hoursArm.eulerAngles.z;
        _initialMinutesRotation = minutesArm.eulerAngles.z;


    }

    // Update is called once per frame
    void Update()
    {
        hoursArm.eulerAngles = GetHoursRotation(dragController.EvaluateX(), _initialHoursRotation, _duration);
        minutesArm.eulerAngles = GetMinutesRotation(dragController.EvaluateX(), _initialMinutesRotation, _duration);
    }

    private Vector3 GetHoursRotation(float evaluatedX, float initalRotation, float totalDuration)
    {
        float hoursPassed = evaluatedX * totalDuration;
        float degreesPassed = hoursPassed * 360f / 12f;

        return new Vector3(0f, 0f, initalRotation - degreesPassed);
    }

    private Vector3 GetMinutesRotation(float evaluatedX, float initialRotation, float totalDuration)
    {
        float hoursPassed = evaluatedX * totalDuration;
        float degreesPassed = hoursPassed * 360f;

        return new Vector3(0f, 0f, initialRotation - degreesPassed);
    }
}
