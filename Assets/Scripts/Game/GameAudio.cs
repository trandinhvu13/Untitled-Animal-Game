using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.UI;

public class GameAudio : MonoBehaviour
{
    #region Audio
    public Audio backgroundAudio;
    public AudioClip backgroundAudioClip;
    public Audio buttonPressAudio;
    public AudioClip buttonPressAudioClip;
    public Toggle soundToggle;
    #endregion

    #region Mono
    void OnEnable()
    {
        GameEvent.instance.OnPlayButtonPress += StopBGMusic;
        GameEvent.instance.OnButtonPress += PlayButtonPressAudio;

        int backgroundMusicID = EazySoundManager.PrepareMusic(backgroundAudioClip, 0.6f, true, false, 2f, 0.5f);
        backgroundAudio = EazySoundManager.GetAudio(backgroundMusicID);
        backgroundAudio.Play();

        int buttonPressAudioID = EazySoundManager.PrepareUISound(buttonPressAudioClip, 1f);
        buttonPressAudio = EazySoundManager.GetUISoundAudio(buttonPressAudioID);
    }
    private void OnDisable()
    {
        GameEvent.instance.OnPlayButtonPress -= StopBGMusic;
        GameEvent.instance.OnButtonPress -= PlayButtonPressAudio;
    }

    void Start()
    {

    }

    void Update()
    {

    }
    #endregion

    #region Methods
    void StopBGMusic()
    {
        backgroundAudio.Stop();
    }

    void PlayButtonPressAudio()
    {
        buttonPressAudio.Play();
    }

    public void AudioToggle()
    {
        if (soundToggle.isOn == false)
        {
            EazySoundManager.GlobalVolume = 0f;
            playerSettings.isMuted = true;
        }
        else if (soundToggle.isOn == true)
        {
            EazySoundManager.GlobalVolume = 1f;
            playerSettings.isMuted = false;
        }

    }
    #endregion
}

