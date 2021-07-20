using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorWipingSound : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayWithoutStop()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }
}
