using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleRotation : MonoBehaviour
{
    public float timeToSwitch = 0.5f;
    private float _stopwatch;

    public Vector3 from;
    public Vector3 to;

    [SerializeField] bool stateFrom;


    // Start is called before the first frame update
    void Start()
    {
        _stopwatch = timeToSwitch;
        stateFrom = true;
    }

    // Update is called once per frame
    void Update()
    {
        _stopwatch -= Time.deltaTime;
        if (_stopwatch <= 0f)
        {
            this.SwitchStates();
            _stopwatch = timeToSwitch;
        }
    }

    void SwitchStates()
    {
        if (stateFrom)
        {
            transform.localEulerAngles = to;
            stateFrom = false;
        }
        else
        {
            transform.localEulerAngles = from;
            stateFrom = true;
        }
    }
}
