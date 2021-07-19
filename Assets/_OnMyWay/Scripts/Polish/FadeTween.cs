using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTween : MonoBehaviour
{
    public float fadeTime = 1f;
    private float _elapsedTime = 0f;

    private bool _isFading;

    Action _onFinishFadeCallback;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        _elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFading)
        {
            _elapsedTime += Time.deltaTime;
            spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                Mathf.Lerp(0f, 1f, _elapsedTime / fadeTime
            ));
            if (_elapsedTime >= fadeTime)
            {
                if (_onFinishFadeCallback != null) _onFinishFadeCallback();
                _elapsedTime = 0f;
                _isFading = false;
            }
        }
    }

    public void StartFading(Action callback)
    {
        this.gameObject.SetActive(true);
        this.spriteRenderer.enabled = true;
        _isFading = true;
        _elapsedTime = 0f;
        _onFinishFadeCallback = callback;
    }
}
