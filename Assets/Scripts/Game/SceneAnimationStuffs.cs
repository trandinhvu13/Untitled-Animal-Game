using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header ("LeanTween Ease Type")]
    public LeanTweenType moveEaseType;
    public LeanTweenType floatingEaseType;
    #endregion

    #region Variable
    bool isStartedUp = true;
    #endregion

    #region Monos
    private void Awake () {
        machine.transform.localPosition = new Vector2 (0, -2f);
        table.transform.localPosition = new Vector2 (0, 3.2f);
        chair.transform.localPosition = new Vector2 (-3f, 0);
        inventory.transform.localPosition = new Vector2 (0, 4.5f);
        restocker.transform.localPosition = new Vector2 (-3f, 0);
        trash.transform.localPosition = new Vector2 (3f, 0);
        pane.transform.localPosition = new Vector2 (0, -2f);

    }

    private void OnEnable () {
        if (isStartedUp) {
            isStartedUp = false;
            LeanTween.moveLocalY (machine, 0, machineMoveTime).setFrom (-2f).setDelay (startUpDelay).setEase (moveEaseType);
            LeanTween.moveLocalY (table, 0, tableMoveTime).setFrom (3.2f).setDelay (startUpDelay).setEase (moveEaseType);
            LeanTween.moveLocalX (chair, -0.009f, chairMoveTime).setFrom (-3f).setDelay (startUpDelay + tableMoveTime).setEase (moveEaseType);
            LeanTween.moveLocalY (inventory, 0, inventoryMoveTime).setFrom (4.5f).setDelay (startUpDelay + tableMoveTime + chairMoveTime).setEase (moveEaseType);
            LeanTween.moveLocalX (restocker, 0, restockerMoveTime).setFrom (-3f).setDelay (startUpDelay + tableMoveTime + chairMoveTime + inventoryMoveTime).setEase (moveEaseType);
            LeanTween.moveLocalX (trash, 0, trashMoveTime).setFrom (3f).setDelay (startUpDelay + tableMoveTime + chairMoveTime + inventoryMoveTime).setEase (moveEaseType);
            LeanTween.moveLocalY (pane, 0, inventoryMoveTime).setFrom (-2f).setDelay (startUpDelay + tableMoveTime + chairMoveTime + inventoryMoveTime).setEase (moveEaseType).setOnComplete(floatingButton);

        }

    }

    private void OnDisable () {
        isStartedUp = true;
        LeanTween.cancelAll(true);
    }
    #endregion

    #region Methods
        void floatingButton(){
            LeanTween.moveLocalY (restocker, 0.04f, floatAmount).setEase (floatingEaseType).setLoopPingPong (-1);
            LeanTween.moveLocalY (trash, 0.04f, floatAmount).setEase (floatingEaseType).setLoopPingPong (-1);
            //LeanTween.moveLocalY (inventory, 0.04f, floatAmount).setEase (floatingEaseType).setLoopPingPong (-1);
        }
    #endregion
}