using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WiggleOnHover : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public float timeToSwitch = 0.5f;
    private float _stopwatch;

    public Vector3 from = new Vector3(0f, 0f, -5f);
    public Vector3 to = new Vector3(0f, 0f, 5f);

    [SerializeField] bool stateFrom;

    bool _isHovering;
    Vector3 _defaultEuler;

    // Start is called before the first frame update
    void Start()
    {
        _stopwatch = timeToSwitch;
        stateFrom = true;

        _defaultEuler = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isHovering) return;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
        _stopwatch = timeToSwitch;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
        transform.localEulerAngles = _defaultEuler;
    }
}
