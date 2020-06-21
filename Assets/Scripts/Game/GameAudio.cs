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

    public Audio scoreCountAudio;
    public AudioClip scoreCountAudioClip;

    public Audio doneScoreCountAudio;
    public AudioClip doneScoreCountAudioClip;

    public Audio cupPickUpAudio;
    public AudioClip cupPickUpAudioClip;

    public Audio cupDropAudio;
    public AudioClip cupDropAudioClip;


    // public Toggle soundToggle;
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
        GameEvent.instance.OnScoreCounting += PlayScoreCount;
        GameEvent.instance.OnDoneScoreCount += PlayDoneScoreCount;
        GameEvent.instance.OnCupPickUp += PlayCupPickUp;
        GameEvent.instance.OnCupDrop += PlayCupDrop;


        backgroundMusicID = EazySoundManager.PrepareMusic(backgroundAudioClip, 0.3f, true, false, 2f, 0.5f);
        backgroundAudio = EazySoundManager.GetAudio(backgroundMusicID);
        backgroundAudio.Play();

        int buttonPressAudioID = EazySoundManager.PrepareUISound(buttonPressAudioClip, 1f);
        buttonPressAudio = EazySoundManager.GetUISoundAudio(buttonPressAudioID);

        int countingAudioID = EazySoundManager.PrepareSound(countingAudioClip,0.15f);
        countingAudio = EazySoundManager.GetSoundAudio(countingAudioID);

        int endCountingAudioID = EazySoundManager.PrepareSound(endCountingAudioClip, 0.15f);
        endCountingAudio = EazySoundManager.GetSoundAudio(endCountingAudioID);

        int spawnAudioID = EazySoundManager.PrepareSound(spawnAudioClip, 0.4f);
        spawnAudio = EazySoundManager.GetSoundAudio(spawnAudioID);

        int despawnAudioID = EazySoundManager.PrepareSound(despawnAudioClip, 0.4f);
        despawnAudio = EazySoundManager.GetSoundAudio(despawnAudioID);

        int correctAudioID = EazySoundManager.PrepareSound(correctAudioClip, 1f);
        correctAudio = EazySoundManager.GetSoundAudio(correctAudioID);

        int incorrectAudioID = EazySoundManager.PrepareSound(incorrectAudioClip, 1f);
        incorrectAudio = EazySoundManager.GetSoundAudio(incorrectAudioID);

        int gameOverAudioID = EazySoundManager.PrepareSound(gameOverAudioClip, 1.4f);
        gameOverAudio = EazySoundManager.GetSoundAudio(gameOverAudioID);

        int pickUpAudioID = EazySoundManager.PrepareSound(pickUpAudioClip, 2f);
        pickUpAudio = EazySoundManager.GetSoundAudio(pickUpAudioID);

        int dropItemAudioID = EazySoundManager.PrepareSound(dropItemAudioClip, 2f);
        dropItemAudio = EazySoundManager.GetSoundAudio(dropItemAudioClip);

        int changeCatAudioID = EazySoundManager.PrepareSound(changeCatAudioClip, 0.8f);
        changeCatAudio = EazySoundManager.GetSoundAudio(changeCatAudioID);

        int scoreCountAudioID = EazySoundManager.PrepareSound(scoreCountAudioClip, 0f,true,null);
        scoreCountAudio = EazySoundManager.GetSoundAudio(scoreCountAudioID);

        int doneScoreCountAudioID = EazySoundManager.PrepareSound(doneScoreCountAudioClip, 0f, false, null);
        doneScoreCountAudio = EazySoundManager.GetSoundAudio(doneScoreCountAudioID);

        int cupPickUpAudioID = EazySoundManager.PrepareSound(cupPickUpAudioClip, 1f);
        cupPickUpAudio = EazySoundManager.GetSoundAudio(cupPickUpAudioID);

        int cupDropAudioID = EazySoundManager.PrepareSound(cupDropAudioClip, 0.5f);
        cupDropAudio = EazySoundManager.GetSoundAudio(cupDropAudioID);
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
        GameEvent.instance.OnScoreCounting -= PlayScoreCount;
        GameEvent.instance.OnDoneScoreCount -= PlayDoneScoreCount;
        GameEvent.instance.OnCupPickUp -= PlayCupPickUp;
        GameEvent.instance.OnCupDrop -= PlayCupDrop;
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
        countingAudio.Stop();
        endCountingAudio.Play();
        backgroundAudio.SetVolume(0.65f);
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
        backgroundAudio.SetVolume(0.3f);
    }

    public void PlayPickUpAudio()
    {
        pickUpAudio.Play();
    }

    public void PlayDropAudio()
    {
        dropItemAudio.Play();
    }

    void PlayCupPickUp()
    {
        cupPickUpAudio.Play();
    }

    void PlayCupDrop()
    {
        cupDropAudio.Play();
    }

    public void PlayChangeCat()
    {
        changeCatAudio.Play();
    }

    void PlayScoreCount()
    {
        scoreCountAudio.Play();
    }

    void PlayDoneScoreCount()
    {
        scoreCountAudio.Stop();
        doneScoreCountAudio.Play();
    }

    

    
    #endregion
}

