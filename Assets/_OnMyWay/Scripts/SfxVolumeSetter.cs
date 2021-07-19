using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SfxVolumeSetter : MonoBehaviour
{
    public AudioMixer sfxMixer;

    // Start is called before the first frame update
    void Start()
    {
        float previousVolume = PlayerPrefs.GetFloat("sfxVolume", 0.75f);
        SetVolume(previousVolume);
    }

    public void SetVolume(float volume)
    {
        sfxMixer.SetFloat("sfxVolume", Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1f)) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
}
