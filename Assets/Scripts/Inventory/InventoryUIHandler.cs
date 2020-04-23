using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUIHandler : MonoBehaviour {
    #region Components
    [SerializeField]
    private TextMeshProUGUI label;
    [SerializeField]
    private GameObject fruitScrollView;
    [SerializeField]
    private GameObject creamScrollView;
    [SerializeField]
    private GameObject drinkScrollView;
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
    private bool isChanged = false;

    #endregion

    #region Monos
    private void OnEnable () {
        SetUpUI ();
    }

    private void OnDisable () {

    }
    private void Start () {
        // fruitScrollView.SetActive(true);
        // creamScrollView.SetActive(true);
        // drinkScrollView.SetActive(true);
    }

    private void Update () {
        if (isChanged) {
            isChanged = false;
            if (currentCategory == 2) {
                GameEvent.instance.ToggleFruitCollider (true);
                GameEvent.instance.ToggleCreamCollider (false);
                GameEvent.instance.ToggleDrinkCollider (false);
                buttonUp.SetActive (false);
                buttonDown.SetActive (true);
                label.text = "Fruit";
            } else if (currentCategory == 0) {
                GameEvent.instance.ToggleFruitCollider (false);
                GameEvent.instance.ToggleCreamCollider (false);
                GameEvent.instance.ToggleDrinkCollider (true);
                buttonUp.SetActive (true);
                buttonDown.SetActive (false);
                label.text = "Drink";
            } else if (currentCategory == 1) {
                GameEvent.instance.ToggleFruitCollider (false);
                GameEvent.instance.ToggleCreamCollider (true);
                GameEvent.instance.ToggleDrinkCollider (false);
                buttonUp.SetActive (true);
                buttonDown.SetActive (true);
                label.text = "Cream";
            }

        }

    }

    #endregion

    #region Collisions
    #endregion

    #region Methods
    void SetUpUI () {
        // fruitScrollView.SetActive(false);
        // creamScrollView.SetActive(false);
        // drinkScrollView.SetActive(false);
        currentCategory = 0;
        scrollViews.transform.position = pos[currentCategory].transform.position;
        label.text = categories[currentCategory];
        buttonUp.SetActive (true);
        buttonDown.SetActive (false);
        GameEvent.instance.ToggleFruitCollider (false);
        GameEvent.instance.ToggleCreamCollider (false);
        GameEvent.instance.ToggleDrinkCollider (true);
    }

    public void MoveUp () {
        currentCategory += 1;
        LeanTween.move (scrollViews, pos[currentCategory].transform.position, transitionTime).setEase (easeType);
        isChanged = true;
    }

    public void MoveDown () {
        currentCategory -= 1;
        LeanTween.move (scrollViews, pos[currentCategory].transform.position, transitionTime).setEase (easeType);
        isChanged = true;
    }
    #endregion

}