using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupState : MonoBehaviour {
    #region Components
    #endregion

    #region Variables
    public bool[] slotIsFull = { false, false, false };
    public int[] answers = { 0, 0, 0 };
    [SerializeField]
    private SpriteRenderer fruit;
    [SerializeField]
    private SpriteRenderer cream;
    [SerializeField]
    private SpriteRenderer drink;
    private Collider2D currentCollided;
    private bool isBeingHeld = false;
    public Vector3 defaultActivePos;
    public int cupID;
    private Vector3 pickupPos;

    //UI
    [SerializeField]
    private string defaultSortingLayer;
    [SerializeField]
    private string pickUpSortingLayer;
    public SpriteRenderer[] sprites = new SpriteRenderer[4];
    public LeanTweenType easeType;
    public float tweenTime;
    #endregion

    #region Monos
    void Awake () { }
    private void OnEnable () {
        SetUp ();
        GameEvent.instance.OnHandleDropItem += HandleDropItem;

    }

    private void OnDisable () {
        GameEvent.instance.OnHandleDropItem -= HandleDropItem;

    }

    void Update () {
        if (isBeingHeld) {
            Hold ();
        }
    }

    #endregion

    #region Collisions
    private void OnTriggerEnter2D (Collider2D other) {
        currentCollided = other;
    }
    private void OnTriggerExit2D (Collider2D other) {
        //currentCollided = null;
    }
    #endregion

    #region Methods
    void SetUp () {
        fruit.sprite = null;
        cream.sprite = null;
        drink.sprite = null;

        for (int i = 0; i < 3; i++) {
            slotIsFull[i] = false;
        }

        for (int i = 0; i < 3; i++) {
            answers[i] = 0;
        }
    }

    private void HandleDropItem (string _type, int _colorID, bool _isDraggable) {
        if (_isDraggable) {
            if (_type == "Fruit") {
                if (slotIsFull[2] == false) {
                    slotIsFull[2] = true;
                    answers[2] = _colorID;
                    fruit.sprite = CurrentInventory.instance.Fruits[_colorID].cup;
                    GameEvent.instance.DecreaseQuantity ("Fruit", _colorID, 1);
                } else {
                    return;
                };
            } else if (_type == "Cream") {
                if (slotIsFull[1] == false) {
                    slotIsFull[1] = true;
                    answers[1] = _colorID;
                    cream.sprite = CurrentInventory.instance.Creams[_colorID].cup;
                    GameEvent.instance.DecreaseQuantity ("Cream", _colorID, 1);
                } else {
                    return;
                };
            } else if (_type == "Drink") {
                if (slotIsFull[0] == false) {
                    slotIsFull[0] = true;
                    answers[0] = _colorID;
                    drink.sprite = CurrentInventory.instance.Drinks[_colorID].cup;
                    GameEvent.instance.DecreaseQuantity ("Drink", _colorID, 1);
                } else {
                    return;
                }
            }
        } else {
            return;
        }
    }

    private void changeSpriteOrder (string _sortingLayer) {
        for (int i = 0; i < 4; i++) {
            sprites[i].sortingLayerName = _sortingLayer;
        }
    }

    public void PickUp () {
        isBeingHeld = true;
        pickupPos = gameObject.transform.position;
        changeSpriteOrder (pickUpSortingLayer);
    }

    public void Hold () {

    }

    public void Drop () {
        if (isBeingHeld) {
            isBeingHeld = false;

            //Check Collision
            if (currentCollided != null) {
                if (currentCollided.gameObject.CompareTag ("Trash")) {
                    changeSpriteOrder (defaultSortingLayer);
                    GameEvent.instance.HandleCup (cupID, false);
                    //gameevent call for cup manager
                } else if (currentCollided.gameObject.CompareTag ("Customer")) {
                    changeSpriteOrder (defaultSortingLayer);
                    Debug.Log("Collided");                    
                    int id = currentCollided.gameObject.GetComponent<CustomerScript>().id;
                    string customerType = currentCollided.gameObject.GetComponent<CustomerScript>().customerType;
                    GameEvent.instance.Compare (answers, id, customerType);
                } else {
                    StartCoroutine (DropSpriteChange ());
                    LeanTween.move (this.gameObject, pickupPos, tweenTime).setEase (easeType);
                }

            } else {
                LeanTween.move (this.gameObject, pickupPos, tweenTime).setEase (easeType);
            }

        }

    }
    IEnumerator DropSpriteChange () {
        yield return new WaitForSeconds (0.4f);
        changeSpriteOrder (defaultSortingLayer);
    }

    #endregion

}