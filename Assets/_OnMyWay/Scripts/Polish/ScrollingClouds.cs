using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingClouds : MonoBehaviour
{
    public float speed;

    // public Transform resetPosition;
    private Vector3 _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.left);
        if (transform.localPosition.x <= 0f)
        {
            transform.localPosition = _startPosition;
        }
    }
}
