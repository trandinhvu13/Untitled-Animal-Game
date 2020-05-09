using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneAnimationStuffs : MonoBehaviour {
    [Header ("Object")]
    #region Objects
    public GameObject restocker;
    public GameObject trash;
    public GameObject inventory;
    public GameObject pane;
    public GameObject machine;
    public GameObject table;
    public GameObject chair;
    public GameObject score;
    public GameObject leanTouch;
    public GameObject fadeBlack;
    public TextMeshProUGUI countDown;
    public GameObject countDownObject;
    public GameObject customerManager;
    public Image fadeBlackImage;
    #endregion

    #region LeanTween
    [Header ("LeanTween Properties")]
    public float floatAmount;
    public float startUpDelay;
    public float machineMoveTime;
    public float restockerMoveTime;
    public float trashMoveTime;
    public float inventoryMoveTime;
    public float paneMoveTime;
    public float tableMoveTime;
    public float chairMoveTime;
    public float scoreMoveTime;
    private int trashHoverID;
    private int restockerHoverID;
    public float fadeAmount;
    public float fadeTime;
    public float countDownTweenTime;

    [Header ("LeanTween Ease Type")]
    public LeanTweenType moveEaseType;
    public LeanTweenType floatingEaseType;
    public LeanTweenType scoreEaseType;
    public LeanTweenType countDownEaseType;
    #endregion

    #region Variable
    bool isStartedUp = true;
    #endregion

    #region Monos
    private void Awake () {
        leanTouch = GameObject.FindWithTag ("LeanTouch");
        machine.transform.localPosition = new Vector2 (0, -2f);
        table.transform.localPosition = new Vector2 (0, 3.2f);
        chair.transform.localPosition = new Vector2 (-3f, 0);
        inventory.transform.localPosition = new Vector2 (0, 4.5f);
        restocker.transform.localPosition = new Vector2 (-3f, 0);
        trash.transform.localPosition = new Vector2 (3f, 0);
        pane.transform.localPosition = new Vector2 (0, -2f);

    }

    private void OnEnable () {
        GameEvent.instance.OnBeginPlay += SetUpBeginPlaying;
        fadeBlack.SetActive (false);
        customerManager.SetActive (false);
        SetActiveTouch (false);
        if (isStartedUp) {
            isStartedUp = false;
            LeanTween.moveLocalY (machine, 0, machineMoveTime).setFrom (-2f).setDelay (startUpDelay).setEase (moveEaseType);
            LeanTween.moveLocalY (table, 0, tableMoveTime).setFrom (3.2f).setDelay (startUpDelay).setEase (moveEaseType);
            LeanTween.moveLocalX (chair, -0.009f, chairMoveTime).setFrom (-3f).setDelay (startUpDelay + tableMoveTime).setEase (moveEaseType);
            LeanTween.moveLocalY (inventory, 0, inventoryMoveTime).setFrom (4.5f).setDelay (startUpDelay + tableMoveTime + chairMoveTime).setEase (moveEaseType);
            LeanTween.moveLocalX (restocker, 0, restockerMoveTime).setFrom (-3f).setDelay (startUpDelay + tableMoveTime + chairMoveTime + inventoryMoveTime).setEase (moveEaseType);
            LeanTween.moveLocalX (trash, 0, trashMoveTime).setFrom (3f).setDelay (startUpDelay + tableMoveTime + chairMoveTime + inventoryMoveTime).setEase (moveEaseType);
            LeanTween.moveLocalY (pane, 0, inventoryMoveTime).setFrom (-2f).setDelay (startUpDelay + tableMoveTime + chairMoveTime + inventoryMoveTime).setEase (moveEaseType);
            LeanTween.moveY (score, 0, scoreMoveTime).setEase (scoreEaseType).setDelay (startUpDelay + tableMoveTime + chairMoveTime + inventoryMoveTime + inventoryMoveTime).setOnComplete (() => { StartCoroutine (CountDown ()); });

        }

    }

    private void OnDisable () {
        GameEvent.instance.OnBeginPlay -= SetUpBeginPlaying;
        isStartedUp = true;
        LeanTween.cancelAll (true);
    }
    #endregion

    #region Methods
    void SetActiveTouch (bool _bool) {
        leanTouch.SetActive (_bool);
    }
    void SetUpBeginPlaying () {
        restockerHoverID = LeanTween.moveLocalY (restocker, 0.04f, floatAmount).setEase (floatingEaseType).setLoopPingPong (-1).id;
        trashHoverID = LeanTween.moveLocalY (trash, 0.04f, floatAmount).setEase (floatingEaseType).setLoopPingPong (-1).id;
        customerManager.SetActive (true);

    }

    IEnumerator CountDown () {
        fadeBlack.SetActive (true);
        LeanTween.value (gameObject, UpdateFadeBlackAlpha, 0, 0, 0);
        LeanTween.value (gameObject, UpdateFadeBlackAlpha, 0, fadeAmount, fadeTime).setEase (LeanTweenType.easeInOutQuad);
        yield return new WaitForSeconds (fadeTime);
        countDown.text = "3";
        countDownObject.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
        LeanTween.scale (countDownObject, new Vector3 (1, 1, 1), countDownTweenTime).setEase (countDownEaseType);
        yield return new WaitForSeconds (0.85f);
        countDown.text = "2";
        countDownObject.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
        LeanTween.scale (countDownObject, new Vector3 (1, 1, 1), countDownTweenTime).setEase (countDownEaseType);
        yield return new WaitForSeconds (0.85f);
        countDown.text = "1";
        countDownObject.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
        LeanTween.scale (countDownObject, new Vector3 (1, 1, 1), countDownTweenTime).setEase (countDownEaseType);
        yield return new WaitForSeconds (0.85f);
        countDown.text = "GO!";
        countDownObject.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
        LeanTween.scale (countDownObject, new Vector3 (1, 1, 1), countDownTweenTime).setEase (countDownEaseType);
        yield return new WaitForSeconds (0.85f);
        countDownObject.transform.localScale = Vector3.zero;
        LeanTween.value (gameObject, UpdateFadeBlackAlpha, fadeAmount, 0, fadeTime).setEase (LeanTweenType.easeInOutQuad).setOnComplete (() => { fadeBlack.SetActive (false); SetActiveTouch (true);; GameStateMachine.instance.ChangeState<InGameState> (); });

    }
    void UpdateFadeBlackAlpha (float val, float ratio) {
        fadeBlackImage.color = new Color (0, 0, 0, val);
    }

    void HandlePauseIn () {

    }

    void HandlePauseOut () {

    }
    #endregion
}