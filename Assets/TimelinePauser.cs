using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinePauser : MonoBehaviour
{
    [SerializeField]
    bool _isPaused = false;
    PlayableDirector playableDirector;

    public List<PlayableDirector> nextScenes;
    [SerializeField]
    private int _transitionTo = 0;

    public int NextScene { get { return _transitionTo; } }

    // Start is called before the first frame update
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        playableDirector.stopped += OnDirectorStop;
    }

    void OnDisable () {
        playableDirector.stopped -= OnDirectorStop;
    }

    public void OnDirectorStop(PlayableDirector pb) {
        Debug.Log(pb.name + " has stopped");

        playableDirector.stopped -= OnDirectorStop;
        nextScenes[NextScene].gameObject.SetActive(true);

        gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(_isPaused) {
            playableDirector.DeferredEvaluate();
        }
    }

    public void PauseTimeline() {
        playableDirector.Pause();
        _isPaused = true;
    }

    public void ResumeTimeline() {
        playableDirector.Resume();
        _isPaused = false;
    }


    public void SetTransitionTo(int index) {
        _transitionTo = index;
    }
}
