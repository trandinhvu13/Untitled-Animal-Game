using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUIHandler : MonoBehaviour {
    #region Components
    [SerializeField]
    private TextMeshProUGUI label;
    #endregion

    #region Variables
    public GameObject scrollViews;
    public string[] categories = new string[3];
    public GameObject[] pos = new GameObject[3];
    [SerializeField]
    private int currentCategory;

    public GameObject buttonUp;
    public GameObject buttonDown;
    public LeanTweenType easeType;
    public float transitionTime;

    #endregion

    #region Monos
    private void OnEnable () {
        SetUpUI ();
    }

    private void OnDisable () {

    }
    private void Start () {

    }

    private void Update () {
        if (currentCategory == 2) {
            buttonUp.SetActive (false);
            buttonDown.SetActive (true);
            label.text = "Drink";
        } else if (currentCategory == 0) {
            buttonUp.SetActive (true);
            buttonDown.SetActive (false);
            label.text = "Fruit";
        } else if (currentCategory == 1){
            buttonUp.SetActive (true);
            buttonDown.SetActive (true);
            label.text = "Cream";
        }
    }

    #endregion

    #region Collisions
    #endregion

    #region Methods
    void SetUpUI () {
        currentCategory = 1;
        scrollViews.transform.position = pos[currentCategory].transform.position;
        label.text = categories[currentCategory];
    }

    public void MoveUp () {
        currentCategory += 1;
        LeanTween.move (scrollViews, pos[currentCategory].transform.position, transitionTime).setEase(easeType);
    }

    public void MoveDown () {
        currentCategory -= 1;
        LeanTween.move (scrollViews, pos[currentCategory].transform.position, transitionTime).setEase(easeType);
    }
    #endregion

}