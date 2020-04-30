using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUIHandler : MonoBehaviour {
    [Header ("Components")]
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
    [Header ("GameObjects")]
    public GameObject scrollViews;
    public GameObject buttonUp;
    public GameObject buttonDown;
    public GameObject[] pos = new GameObject[3];

    [Header ("Variables")]
    public string[] categories = new string[3];
    [SerializeField]
    private int currentCategory;
    private bool isChanged = false;
    [Header ("LeanTween")]
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
        LeanTween.scale(buttonUp,new Vector3(5.5f,5.5f,5.5f), 0.2f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong(1);
        LeanTween.move (scrollViews, pos[currentCategory].transform.position, transitionTime).setEase (easeType);
        isChanged = true;
    }

    public void MoveDown () {
        currentCategory -= 1;
        LeanTween.scale(buttonDown,new Vector3(5.5f,5.5f,5.5f), 0.2f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong(1);
        LeanTween.move (scrollViews, pos[currentCategory].transform.position, transitionTime).setEase (easeType);
        isChanged = true;
    }
    #endregion

}