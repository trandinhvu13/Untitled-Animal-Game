﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    #region Game Objects
    public GameObject transitionBall;
    #endregion

    #region Variables
    Scene currentScene;
    Scene nextScene;
    #endregion

    #region Tween
    public LeanTweenType transitionEase;
    public float transitionTime;
    #endregion

    #region Monos
    private void OnEnable () {
        transitionBall.transform.localScale = Vector3.zero;
        currentScene = SceneManager.GetActiveScene ();
        GameEvent.instance.OnChangeScene += HandleChangeScene;
        SceneManager.sceneLoaded += HandleSceneChanged;
    }
    private void OnDisable () {
        GameEvent.instance.OnChangeScene -= HandleChangeScene;
        SceneManager.sceneLoaded -= HandleSceneChanged;
    }
    #endregion

    #region Methods
    void HandleChangeScene (int sceneID) {
        if (sceneID == 3) {
            LeanTween.scale (transitionBall, new Vector3 (7, 7, 9), 0).setEase (transitionEase).setIgnoreTimeScale (true).setOnComplete (() => { Time.timeScale = 1; SceneManager.LoadScene (1); });
        } else {
            LeanTween.scale (transitionBall, new Vector3 (7, 7, 9), transitionTime).setFrom (new Vector3 (0, 0, 9)).setEase (transitionEase).setIgnoreTimeScale (true).setOnComplete (() => { Time.timeScale = 1; SceneManager.LoadScene (sceneID); });
        }

    }
    IEnumerator LoadYourAsyncScene (int id) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (id, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone) {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;

    }
    void HandleSceneChanged (Scene scene, LoadSceneMode mode) {

        if (scene.buildIndex == 1) {
            currentScene = SceneManager.GetSceneByBuildIndex (scene.buildIndex);
            LeanTween.scale (transitionBall, new Vector3 (0, 0, 0), transitionTime).setEase (transitionEase).setOnComplete (() => { GameStateMachine.instance.ChangeState<StartGameState> (); });
        } else if (scene.buildIndex == 0) {
            currentScene = SceneManager.GetSceneByBuildIndex (scene.buildIndex);
            LeanTween.scale (transitionBall, new Vector3 (0, 0, 0), transitionTime).setEase (transitionEase).setOnComplete (() => { GameStateMachine.instance.ChangeState<MenuState> (); });

        } else if (scene.buildIndex == 2) {
            GameStateMachine.instance.ChangeState<ResetGameState> ();
        }

        #endregion

    }
}