using System;
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
        LeanTween.scale (transitionBall, new Vector3 (7, 7, 7), transitionTime).setFrom (Vector3.zero).setEase (transitionEase).setOnComplete (() => { SceneManager.LoadScene(sceneID); });
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
        if (scene.buildIndex != currentScene.buildIndex) {
            currentScene = SceneManager.GetSceneByBuildIndex (scene.buildIndex);
            LeanTween.scale (transitionBall, new Vector3 (0, 0, 0), transitionTime).setEase (transitionEase).setOnComplete (() => { Debug.Log ("change state"); });
        }
    }

    #endregion

}