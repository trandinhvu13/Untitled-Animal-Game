using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentInventory : MonoBehaviour {
    #region Variables
    public static CurrentInventory instance = null;

    public List<Drink> Drinks = new List<Drink> ();
    public List<Cream> Creams = new List<Cream> ();
    public List<Fruit> Fruits = new List<Fruit> ();

    #endregion

    #region Methods

    void FirstSetupInventory () {
        for (int i = 1; i < 9; i++) {
            Drinks[i].MaxQuantity = PlayerStats.instance.drinkMQuantity;
            Drinks[i].Quantity = Drinks[i].MaxQuantity;
        }
        for (int i = 1; i < 9; i++) {
            Creams[i].MaxQuantity = PlayerStats.instance.creamMQuantity;
            Creams[i].Quantity = Creams[i].MaxQuantity;
        }
        for (int i = 1; i < 9; i++) {
            Fruits[i].MaxQuantity = PlayerStats.instance.fruitMQuantity;
            Fruits[i].Quantity = Fruits[i].MaxQuantity;
        }

        //Set up color id
        for (int i = 1; i < 9; i++) {
            Drinks[i].ColorID = i;
            Creams[i].ColorID = i;
            Fruits[i].ColorID = i;
        }
    }

    void IncreaseQuantityToMax (string _type, int _colorId) {
        if (_type == "Drink") {
            Drinks[_colorId].Quantity = Drinks[_colorId].MaxQuantity;
        } else if (_type == "Cream") {
            Creams[_colorId].Quantity = Creams[_colorId].MaxQuantity;
        } else if (_type == "Fruit") {
            Fruits[_colorId].Quantity = Fruits[_colorId].MaxQuantity;
        }

    }

    void DecreaseQuantity (string _type, int _colorId, int _amount) {
        if (_type == "Drink") {
            Drinks[_colorId].Quantity -= _amount;
        } else if (_type == "Cream") {
            Creams[_colorId].Quantity -= _amount;
        } else if (_type == "Fruit") {
            Fruits[_colorId].Quantity -= _amount;
        }
    }

    void IncreaseMaxQuantity (string _type, int _amount) {
        if (_type == "Drink") {
            PlayerStats.instance.drinkMQuantity += _amount;
        } else if (_type == "Cream") {
            PlayerStats.instance.creamMQuantity += _amount;
        } else if (_type == "Fruit") {
            PlayerStats.instance.fruitMQuantity += _amount;
        }

    }
    #endregion

    #region MonoBehaviour
    private void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }

    }

    private void OnEnable () {
        FirstSetupInventory ();
        GameEvent.instance.OnIncreaseQuantityToMax += IncreaseQuantityToMax;
        GameEvent.instance.OnDecreaseQuantity += DecreaseQuantity;
        GameEvent.instance.OnIncreaseMaxQuantity += IncreaseMaxQuantity;

    }

    private void OnDisable () {
        GameEvent.instance.OnIncreaseQuantityToMax -= IncreaseQuantityToMax;
        GameEvent.instance.OnDecreaseQuantity -= DecreaseQuantity;
        GameEvent.instance.OnIncreaseMaxQuantity -= IncreaseMaxQuantity;
    }

    private void Start () {

    }
    #endregion

}