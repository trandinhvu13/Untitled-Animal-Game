﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupManager : MonoBehaviour {
    #region Components
    #endregion

    #region  Variables
    [SerializeField]
    private GameObject cup1;
    [SerializeField]
    private GameObject cup2;
    [SerializeField]
    private Transform activePos;
    [SerializeField]
    private Transform inactivePos;
    public LeanTweenType easeType;
    public float tweenTime;
    public float delayTweenTime;
    #endregion

    #region Monos

    private void OnEnable () {
        GameEvent.instance.OnHandleCup += HandleCups;
        cup1.transform.position = activePos.position;
        cup2.transform.position = inactivePos.position;
        cup1.SetActive(true);
        cup2.SetActive(false);

    }

    private void OnDisable () {
        GameEvent.instance.OnHandleCup -= HandleCups;
    }
    #endregion

    #region Methods
    private void HandleCups (int cupID, bool state) {
        if (cupID == 1) {
            cup1.SetActive(state);
            cup1.transform.position = inactivePos.position;
            cup2.SetActive(true);
            LeanTween.move(cup2, activePos, tweenTime).setDelay(delayTweenTime).setEase(easeType);
        }else if(cupID==2){
            cup2.SetActive(state);
            cup2.transform.position = inactivePos.position;
            cup1.SetActive(true);
            LeanTween.move(cup1, activePos, tweenTime).setDelay(delayTweenTime).setEase(easeType);
        }
    }
    #endregion
}