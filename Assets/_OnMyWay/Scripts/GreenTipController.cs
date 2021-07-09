using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GreenTipController : MonoBehaviour
{
    public ClickableElement greenTipButton;
    public ClickableElement tipConfirmButton;
    public ClickableElement tipExitButton;

    [Header("Fade values")]
    public float fadeTime = 1f;
    private float _elapsedTime = 0;

    public AnimationCurve lerpCurve;

    [Header("Background")]
    public SpriteRenderer journalBackground;
    public float[] alphaRange = { 0f, 0.1f };

    [Header("Journal position")]
    public Transform journal;
    public Vector3[] positions = { new Vector3(0f, -10f, 0f), new Vector3(0f, 0f, 0f) };


    [Header("FadeIn Callbacks")]
    public UnityEvent onStartFadeInResponse;
    public UnityEvent onEndFadeInResponse;
    [Header("FadeOut Callbacks")]
    public UnityEvent onStartFadeOutResponse;
    public UnityEvent onEndFadeOutResponse;


    TimelineController timelineController;

    // Start is called before the first frame update
    void Start()
    {
        timelineController = FindObjectOfType<TimelineController>();

        greenTipButton.onPointerUpCallback += () =>
        {
            this.gameObject.SetActive(true);
            this.StartFadeIn();
        };

        tipConfirmButton.onPointerUpCallback += () =>
        {
            this.StartFadeOut(() =>
            {
                timelineController.SetGreenTipOpen(false);
            });
        };
        tipExitButton.onPointerUpCallback += () =>
        {
            this.StartFadeOut(() =>
            {
                timelineController.SetGreenTipOpen(false);
            });
        };

        gameObject.SetActive(false);
        // StartFadeIn(() =>
        // {
        //     Debug.Log("Finished fading in");
        //     StartFadeOut(() =>
        //     {
        //         Debug.Log("Finished fading out");
        //     });
        // });
    }

    private void DisableButtons() {
        tipConfirmButton.enabled = false;
        tipExitButton.enabled = false;
    }


    public void StartFadeIn(Action callback = null)
    {
        timelineController.SetGreenTipOpen(true);

        _elapsedTime = 0;

        onStartFadeInResponse?.Invoke();
        StartCoroutine(FadeIn(callback));
    }

    private IEnumerator FadeIn(Action callback)
    {
        yield return new WaitUntil(() => UpdateFadeIn(Time.deltaTime));

        if (callback != null)
        {
            callback();
        }
        onEndFadeInResponse?.Invoke();
    }

    private bool UpdateFadeIn(float deltaTime)
    {
        _elapsedTime += deltaTime;
        journal.position = Vector3.Lerp(positions[0], positions[1], lerpCurve.Evaluate(_elapsedTime / fadeTime));
        journalBackground.color = new Color(
            journalBackground.color.r, journalBackground.color.g, journalBackground.color.b,
            Mathf.Lerp(alphaRange[0], alphaRange[1], lerpCurve.Evaluate(_elapsedTime / fadeTime))
        );
        if (_elapsedTime > fadeTime)
        {
            _elapsedTime = 0f;
            return true;
        }
        else
            return false;
    }


    public void StartFadeOut(Action callback = null)
    {
        _elapsedTime = 0;

        onStartFadeOutResponse?.Invoke();
        StartCoroutine(FadeOut(callback));
    }

    private IEnumerator FadeOut(Action callback)
    {
        yield return new WaitUntil(() => UpdateFadeOut(Time.deltaTime));

        if (callback != null)
        {
            callback();
        }
        onEndFadeOutResponse?.Invoke();
    }

    private bool UpdateFadeOut(float deltaTime)
    {
        _elapsedTime += deltaTime;
        journal.position = Vector3.Lerp(positions[1], positions[0], lerpCurve.Evaluate(_elapsedTime / fadeTime));
        journalBackground.color = new Color(
            journalBackground.color.r, journalBackground.color.g, journalBackground.color.b,
            Mathf.Lerp(alphaRange[1], alphaRange[0], lerpCurve.Evaluate(_elapsedTime / fadeTime))
        );
        if (_elapsedTime > fadeTime)
        {
            _elapsedTime = 0f;
            return true;
        }
        else
            return false;
    }
}
