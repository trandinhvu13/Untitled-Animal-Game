using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupState : MonoBehaviour {
    public bool[] slotIsFull = { false, false, false };
    public int[] answers = { 0, 0, 0 };
    [SerializeField]
    private SpriteRenderer fruit;
    [SerializeField]
    private SpriteRenderer cream;
    [SerializeField]
    private SpriteRenderer drink;

    void Awake () {
    }
    private void OnEnable () {
        SetUp ();
    }

    private void OnDisable () {

    }

    void Update () {

    }

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
    private void OnTriggerEnter2D (Collider2D collision) {
        string tag = collision.gameObject.tag;
        if (tag == "Drink") {
            if (slotIsFull[0] == false) {
                slotIsFull[0] = true;
                int colorID = collision.GetComponent<InventoryDrinkItem> ().scriptableObject.ColorID;
                answers[0] = colorID;
                drink.sprite = CurrentInventory.instance.Drinks[colorID].cup;
                GameEvent.instance.DecreaseQuantity ("Drink", colorID, 1);
            } else {
                return;
            };
        } else if (tag == "Cream") {

            if (slotIsFull[1] == false) {
                slotIsFull[1] = true;
                int colorID = collision.GetComponent<InventoryCreamItem> ().scriptableObject.ColorID;
                answers[1] = colorID;
                cream.sprite = CurrentInventory.instance.Creams[colorID].cup;
                GameEvent.instance.DecreaseQuantity ("Cream", colorID, 1);
            } else {
                return;
            };
        } else if (tag == "Fruit") {

            if (slotIsFull[2] == false) {
                slotIsFull[2] = true;
                int colorID = collision.GetComponent<InventoryFruitItem> ().scriptableObject.ColorID;
                answers[2] = colorID;
                fruit.sprite = CurrentInventory.instance.Fruits[colorID].cup;
                GameEvent.instance.DecreaseQuantity ("Fruit", colorID, 1);
            } else {
                return;
            };
        }

    }
}