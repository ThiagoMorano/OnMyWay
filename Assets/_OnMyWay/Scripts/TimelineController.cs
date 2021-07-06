using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class TimelineController : MonoBehaviour
{
    public Action onDirectorFinishedCallback;

    [SerializeField] bool _isPaused = false;
    PlayableDirector playableDirector;


    bool _greenTipOpen = false;

    public void SetGreenTipOpen(bool value) {
        _greenTipOpen = value;
    }

    // public List<PlayableDirector> nextScenes;
    // [SerializeField]
    // private int _transitionTo = 0;

    // public int NextScene { get { return _transitionTo; } }

    // Start is called before the first frame update
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        playableDirector.stopped += OnDirectorFinished;
    }

    void OnDisable()
    {
        playableDirector.stopped -= OnDirectorFinished;
    }

    public void OnDirectorFinished(PlayableDirector pb)
    {
        if (onDirectorFinishedCallback != null) onDirectorFinishedCallback();

        playableDirector.stopped -= OnDirectorFinished;

        gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (_isPaused)
        {
            playableDirector.DeferredEvaluate();
        }
    }

    public void PauseTimeline()
    {
        playableDirector.Pause();
        _isPaused = true;
    }

    public void ResumeTimeline()
    {
        if (CanResume())
        {
            playableDirector.Resume();
            _isPaused = false;
        }
    }

    private bool CanResume()
    {
        // return !(_greenTipOpen || _settingsOpen);
        return !_greenTipOpen;
        // return true;
    }
}
