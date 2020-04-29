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

    }

    private void OnEnable () {
        if (isStartedUp) {
            isStartedUp = false;

        }

    }

    private void OnDisable () {
        isStartedUp = true;
    }
    #endregion
}