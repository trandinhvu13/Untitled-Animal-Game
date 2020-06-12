﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour {
    #region Game Objects
    public GameObject playButton;
    public GameObject scoreButton;
    public GameObject howToButton;
    public GameObject optionsButton;
    public GameObject aboutButton;
    public GameObject title;
    public GameObject character1;
    public GameObject character2;
    public GameObject scorePanel;
    public GameObject howToPanel;
    public GameObject optionPanel;
    public GameObject aboutPanel;

    #endregion

    #region LeanTween
    public LeanTweenType buttonEase;
    public float buttonTweenTime;
    public LeanTweenType titleStartUpEase;
    public float titleStartUpTweenTime;
    public float titleStartUpDelayTime;
    public LeanTweenType buttonStartUpEase;
    public float buttonStartUpTweenTime;
    public float buttonStartUpDelay;
    public LeanTweenType charStartUpEase;
    public float charStartUpTweenTime;
    public float charStartUpDelay;
    public LeanTweenType panelMoveEase;
    public float panelMoveTweenTime;

    #endregion

    #region Monos
    private void OnEnable () {

        MainMenuCharacter ();
        SetUpTween ();
    }
    #endregion

    #region Methods

    public void MainMenuCharacter () {
        int rand1 = UnityEngine.Random.Range (0, 39);
        int rand2 = UnityEngine.Random.Range (0, 39);
        character1.GetComponent<SpriteRenderer> ().sprite = CustomerDatabase.instance.customer[rand1].sprite;
        character2.GetComponent<SpriteRenderer> ().sprite = CustomerDatabase.instance.customer[rand2].sprite;
        character1.GetComponent<Animator> ().runtimeAnimatorController = CustomerDatabase.instance.customer[rand1].animationController;
        character2.GetComponent<Animator> ().runtimeAnimatorController = CustomerDatabase.instance.customer[rand2].animationController;
    }

    public void SetUpTween () {
        playButton.transform.localScale = Vector3.zero;
        title.transform.localScale = Vector3.zero;
        playButton.transform.localScale = Vector3.zero;
        howToButton.transform.localScale = Vector3.zero;
        scoreButton.transform.localScale = Vector3.zero;
        optionsButton.transform.localScale = Vector3.zero;
        aboutButton.transform.localScale = Vector3.zero;

        LeanTween.scale (title, new Vector3 (0.37586f, 0.37586f, 0.37586f), titleStartUpTweenTime).setEase (titleStartUpEase).setFrom (Vector3.zero).setDelay (titleStartUpDelayTime);
        LeanTween.scale (playButton, new Vector3 (7.5f, 7.5f, 7.5f), buttonStartUpTweenTime).setEase (buttonStartUpEase).setFrom (Vector3.zero).setDelay (titleStartUpDelayTime + buttonStartUpDelay);
        LeanTween.scale (scoreButton, new Vector3 (7.5f, 7.5f, 7.5f), buttonStartUpTweenTime).setEase (buttonStartUpEase).setFrom (Vector3.zero).setDelay (titleStartUpDelayTime + buttonStartUpDelay + 0.3f);
        LeanTween.scale (howToButton, new Vector3 (7.5f, 7.5f, 7.5f), buttonStartUpTweenTime).setEase (buttonStartUpEase).setFrom (Vector3.zero).setDelay (titleStartUpDelayTime + buttonStartUpDelay + 0.45f);
        LeanTween.scale (optionsButton, new Vector3 (7.5f, 7.5f, 7.5f), buttonStartUpTweenTime).setEase (buttonStartUpEase).setFrom (Vector3.zero).setDelay (titleStartUpDelayTime + buttonStartUpDelay + 0.6f);
        LeanTween.scale (aboutButton, new Vector3 (7.5f, 7.5f, 7.5f), buttonStartUpTweenTime).setEase (buttonStartUpEase).setFrom (Vector3.zero).setDelay (titleStartUpDelayTime + buttonStartUpDelay + 0.75f);
        LeanTween.moveLocalY (character1, -4.29f, charStartUpTweenTime).setEase (charStartUpEase).setDelay (charStartUpDelay);
        LeanTween.moveLocalY (character2, -4.29f, charStartUpTweenTime).setEase (charStartUpEase).setDelay (charStartUpDelay);

    }

    public void playClick (GameObject _gameObject) {
        if (!LeanTween.isTweening (_gameObject)) {
            LeanTween.scale (_gameObject, new Vector3 (9, 9, 9), buttonTweenTime).setLoopPingPong (1).setEase (buttonEase).setOnComplete (execute);
        }

        void execute () {
            GameEvent.instance.PlayButtonPress ();
        }
    }

    // public void upgradeClick (GameObject _gameObject) {
    //     if (!LeanTween.isTweening (_gameObject)) {
    //         LeanTween.scale (_gameObject, new Vector3 (9, 9, 9), buttonTweenTime).setLoopPingPong (1).setEase (buttonEase).setOnComplete (execute);
    //     }

    //     void execute () {
    //         MoveMenu ("left");
    //     }
    // }

    public void scoreClick (GameObject _gameObject) {
        if (!LeanTween.isTweening (_gameObject)) {
            LeanTween.scale (_gameObject, new Vector3 (9, 9, 9), buttonTweenTime).setLoopPingPong (1).setEase (buttonEase).setOnComplete (execute);
        }

        void execute () {
            MoveMenu ("left");
            MovePanel (scorePanel, "left");

        }
    }

    public void howToClick (GameObject _gameObject) {
        if (!LeanTween.isTweening (_gameObject)) {
            LeanTween.scale (_gameObject, new Vector3 (9, 9, 9), buttonTweenTime).setLoopPingPong (1).setEase (buttonEase).setOnComplete (execute);
        }

        void execute () {
            MoveMenu ("left");
        }
    }

    public void optionsClick (GameObject _gameObject) {
        if (!LeanTween.isTweening (_gameObject)) {
            LeanTween.scale (_gameObject, new Vector3 (9, 9, 9), buttonTweenTime).setLoopPingPong (1).setEase (buttonEase).setOnComplete (execute);
        }

        void execute () {
            MoveMenu ("left");
        }
    }

    public void aboutClick (GameObject _gameObject) {
        if (!LeanTween.isTweening (_gameObject)) {
            LeanTween.scale (_gameObject, new Vector3 (9, 9, 9), buttonTweenTime).setLoopPingPong (1).setEase (buttonEase).setOnComplete (execute);
        }

        void execute () {
            MoveMenu ("left");
        }
    }

    public void closeClick (GameObject _gameObject) {
        if (!LeanTween.isTweening (_gameObject)) {
            LeanTween.scale (_gameObject, new Vector3 (1.2f, 1.2f, 1.2f), buttonTweenTime).setLoopPingPong (1).setEase (buttonEase).setOnComplete (execute);
        }

        void execute () {
            MoveMenu ("right");
            MovePanel(scorePanel, "right");
        }
    }

    public void MovePanel (GameObject _panel, string _dir) {
        if (_dir == "left") {
            LeanTween.moveX (_panel, 0, panelMoveTweenTime).setEase (panelMoveEase);
        } else if (_dir == "right") {
            LeanTween.moveX (_panel, 2.5f, panelMoveTweenTime).setEase (panelMoveEase);
        }
    }

    public void MoveMenu (string _dir) {
        if (_dir == "left") {
            LeanTween.moveX (gameObject, -2, panelMoveTweenTime).setEase (panelMoveEase);
        } else if (_dir == "right") {
            LeanTween.moveX (gameObject, 0, panelMoveTweenTime).setEase (panelMoveEase);
        }
    }
    #endregion
}