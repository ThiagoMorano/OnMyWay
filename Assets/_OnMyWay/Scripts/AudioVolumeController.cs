using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioVolumeController : MonoBehaviour
{
    // [System.Serializable]
    // enum MixerType {
    //     music,
    //     sfx
    // }

    public HorizontalSlider musicSlider;
    private DraggableItem musicHandler;
    private MusicVolumeSetter musicVolumeSetter;


    public HorizontalSlider sfxSlider;
    private DraggableItem sfxHandler;
    private SfxVolumeSetter sfxVolumeSetter;

    // private MusicVolume musicVolume;


    void Start()
    {
        musicVolumeSetter = FindObjectOfType<MusicVolumeSetter>();

        musicHandler = musicSlider.handler.GetComponent<DraggableItem>();
        musicHandler.onDragCallback += AdjustMusicVolume;

        float previousMusicVolume = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        musicSlider.SetValue(previousMusicVolume);


        sfxVolumeSetter = FindObjectOfType<SfxVolumeSetter>();

        sfxHandler = sfxSlider.handler.GetComponent<DraggableItem>();
        sfxHandler.onDragCallback += AdjusSfxVolume;

        float previousSfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.75f);
        sfxSlider.SetValue(previousSfxVolume);
    }

    private void AdjustMusicVolume()
    {
        float handlerValue = musicSlider.EvaluateX();
        musicVolumeSetter?.SetVolume(handlerValue);
    }


    private void AdjusSfxVolume()
    {
        float handlerValue = sfxSlider.EvaluateX();
        sfxVolumeSetter?.SetVolume(handlerValue);
    }
}
