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


    [SerializeField] bool _greenTipOpen = false;
    [SerializeField] bool _waitingForTask = false;
    [SerializeField] bool _nextButtonOpen = false;
    [SerializeField] bool _settingsOpen = false;

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


    private void AttemptResume()
    {
        if (CanResume())
        {
            ResumeTimeline();
        }
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
        return !(_greenTipOpen || _waitingForTask || _nextButtonOpen || _settingsOpen);
        // return true;
    }

    public void SetGreenTipOpen(bool value)
    {
        _greenTipOpen = value;
        if (!_greenTipOpen)
        {
            AttemptResume();
        }
    }

    public void SetWaitingForTask(bool value)
    {
        _waitingForTask = value;
        if (!_waitingForTask)
        {
            AttemptResume();
        }
    }

    public void SetSettingsOpen(bool value)
    {
        _settingsOpen = value;
        if (!_settingsOpen)
        {
            AttemptResume();
        }
    }

    public void SetNextButtonOpen(bool value)
    {
        _nextButtonOpen = value;
        if (!_nextButtonOpen)
        {
            AttemptResume();
        }
    }
}
