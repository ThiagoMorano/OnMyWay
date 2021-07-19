using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicVolumeSetter : MonoBehaviour
{
    public AudioMixer musicMixer;

    // Start is called before the first frame update
    void Start()
    {
        float previousVolume = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        SetVolume(previousVolume);
    }

    public void SetVolume(float volume)
    {
        musicMixer.SetFloat("musicVolume", Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1f)) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
}
