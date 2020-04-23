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

    //UI
    [SerializeField]
    private string defaultSortingLayer;
    [SerializeField]
    private string pickUpSortingLayer;
    public SpriteRenderer[] sprites = new SpriteRenderer[4];
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

    private void HandleDropItem (string _type, int _colorID) {
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
    }

    private void changeSpriteOrder (string _sortingLayer) {
        for (int i = 0; i < 4; i++) {
            sprites[i].sortingLayerName = _sortingLayer;
        }
    }

    public void PickUp () {
        changeSpriteOrder (pickUpSortingLayer);
    }

    public void Hold () {

    }

    public void Drop () {
        changeSpriteOrder (defaultSortingLayer);
    }
    #endregion

}