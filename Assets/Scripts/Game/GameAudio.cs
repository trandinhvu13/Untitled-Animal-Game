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
    int backgroundMusicID;

    public Audio buttonPressAudio;
    public AudioClip buttonPressAudioClip;

    public Audio countingAudio;
    public AudioClip countingAudioClip;

    public Audio endCountingAudio;
    public AudioClip endCountingAudioClip;

    public Audio spawnAudio;
    public AudioClip spawnAudioClip;

    public Audio despawnAudio;
    public AudioClip despawnAudioClip;

    public Audio correctAudio;
    public AudioClip correctAudioClip;

    public Audio incorrectAudio;
    public AudioClip incorrectAudioClip;

    public Audio gameOverAudio;
    public AudioClip gameOverAudioClip;

    public Audio pickUpAudio;
    public AudioClip pickUpAudioClip;

    public Audio dropItemAudio;
    public AudioClip dropItemAudioClip;

    public Audio changeCatAudio;
    public AudioClip changeCatAudioClip;

    public Toggle soundToggle;
    #endregion

    #region Mono
    void OnEnable()
    {
        GameEvent.instance.OnPlayButtonPress += StopBGMusic;
        GameEvent.instance.OnButtonPress += PlayButtonPressAudio;
        GameEvent.instance.OnCustomerSpawn += PlaySpawnAudio;
        GameEvent.instance.OnCustomerDespawn += PlayDespawnAudio;
        GameEvent.instance.OnCorrect += PlayCorrectAudio;
        GameEvent.instance.OnFalse += PlayIncorrectAudio;
        GameEvent.instance.OnGameOver += PlayGameOverAudio;
        GameEvent.instance.OnPickUpItem += PlayPickUpAudio;
        GameEvent.instance.OnItemDrop += PlayDropAudio;
        GameEvent.instance.OnCountingStart += PlayCountingAudio;
        GameEvent.instance.OnDoneCountingStart += PlayEndCountingAudio;
        GameEvent.instance.OnChangeCat += PlayChangeCat;


        backgroundMusicID = EazySoundManager.PrepareMusic(backgroundAudioClip, 0.4f, true, false, 2f, 0.5f);
        backgroundAudio = EazySoundManager.GetAudio(backgroundMusicID);
        backgroundAudio.Play();

        int buttonPressAudioID = EazySoundManager.PrepareUISound(buttonPressAudioClip, 1f);
        buttonPressAudio = EazySoundManager.GetUISoundAudio(buttonPressAudioID);

        int countingAudioID = EazySoundManager.PrepareSound(countingAudioClip,1.4f);
        countingAudio = EazySoundManager.GetSoundAudio(countingAudioID);

        int endCountingAudioID = EazySoundManager.PrepareSound(endCountingAudioClip, 1.4f);
        endCountingAudio = EazySoundManager.GetSoundAudio(endCountingAudioID);

        int spawnAudioID = EazySoundManager.PrepareSound(spawnAudioClip, 1f);
        spawnAudio = EazySoundManager.GetSoundAudio(spawnAudioID);

        int despawnAudioID = EazySoundManager.PrepareSound(despawnAudioClip, 1f);
        despawnAudio = EazySoundManager.GetSoundAudio(despawnAudioID);

        int correctAudioID = EazySoundManager.PrepareSound(correctAudioClip, 1f);
        correctAudio = EazySoundManager.GetSoundAudio(correctAudioID);

        int incorrectAudioID = EazySoundManager.PrepareSound(incorrectAudioClip, 1f);
        incorrectAudio = EazySoundManager.GetSoundAudio(incorrectAudioID);

        int gameOverAudioID = EazySoundManager.PrepareSound(gameOverAudioClip, 1f);
        gameOverAudio = EazySoundManager.GetSoundAudio(gameOverAudioID);

        int pickUpAudioID = EazySoundManager.PrepareSound(pickUpAudioClip, 1f);
        pickUpAudio = EazySoundManager.GetSoundAudio(pickUpAudioID);

        int dropItemAudioID = EazySoundManager.PrepareSound(dropItemAudioClip, 1f);
        dropItemAudio = EazySoundManager.GetSoundAudio(dropItemAudioClip);

        int changeCatAudioID = EazySoundManager.PrepareSound(changeCatAudioClip, 1f);
        changeCatAudio = EazySoundManager.GetSoundAudio(changeCatAudioID);
    }
    private void OnDisable()
    {
        GameEvent.instance.OnPlayButtonPress -= StopBGMusic;
        GameEvent.instance.OnButtonPress -= PlayButtonPressAudio;
        GameEvent.instance.OnCustomerSpawn -= PlaySpawnAudio;
        GameEvent.instance.OnCustomerDespawn -= PlayDespawnAudio;
        GameEvent.instance.OnCorrect -= PlayCorrectAudio;
        GameEvent.instance.OnFalse -= PlayIncorrectAudio;
        GameEvent.instance.OnGameOver -= PlayGameOverAudio;
        GameEvent.instance.OnPickUpItem -= PlayPickUpAudio;
        GameEvent.instance.OnItemDrop -= PlayDropAudio;
        GameEvent.instance.OnCountingStart -= PlayCountingAudio;
        GameEvent.instance.OnDoneCountingStart -= PlayEndCountingAudio;
        GameEvent.instance.OnChangeCat -= PlayChangeCat;
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

    void PlayCountingAudio()
    {
        countingAudio.Play();
    }

    void PlayEndCountingAudio()
    {
        endCountingAudio.Play();
        backgroundAudio.SetVolume(0.6f);
    }

    void PlaySpawnAudio()
    {
        spawnAudio.Play();
    }

    void PlayDespawnAudio()
    {
        despawnAudio.Play();
    }

    void PlayCorrectAudio()
    {
        correctAudio.Play();
    }

    void PlayIncorrectAudio()
    {
        incorrectAudio.Play();
    }

    void PlayGameOverAudio()
    {
        gameOverAudio.Play();
        backgroundAudio.SetVolume(0.2f);
    }

    public void PlayPickUpAudio()
    {
        pickUpAudio.Play();
    }

    public void PlayDropAudio()
    {
        dropItemAudio.Play();
    }

    public void PlayChangeCat()
    {
        changeCatAudio.Play();
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

