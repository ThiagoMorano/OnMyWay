using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TweenPositions : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;

    public float tweenTime;
    public AnimationCurve animationCurve;

    private float _elapsedTime = 0f;

    public bool animateOnStart = false;
    private bool _isTweening = false;

    public UnityEvent OnStartResponse;
    public UnityEvent OnEndResponse;

    // Start is called before the first frame update
    void Start()
    {
        if (animateOnStart)
        {
            StartTween();
        }
    }

    public void StartTween()
    {
        _isTweening = true;
        OnStartResponse?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTweening)
        {
            _elapsedTime += Time.deltaTime;
            EvaluateAnimation();
        }
    }

    private void EvaluateAnimation()
    {
        transform.position = Vector3.Lerp(start, end, animationCurve.Evaluate(_elapsedTime / tweenTime));
        if(_elapsedTime >= tweenTime) {
            EndAnimation();
        }
    }

    private void EndAnimation()
    {
        _elapsedTime = 0;
        _isTweening = false;
        OnEndResponse?.Invoke();
    }
}
