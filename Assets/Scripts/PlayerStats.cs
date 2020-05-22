using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public static PlayerStats instance = null;

    private void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    [Header ("Score")]
    public int totalScore;
    public int ordersToIncreaseMult;
    public int maxMultiplier;
    public int maxLife;
    public int highScore = 0;

    [Header ("Inventory")]
    public int drinkMQuantity;
    public int creamMQuantity;
    public int fruitMQuantity;

    [Header ("Customer")]
    public float waitTime;
    public float customerWaitingTime = 10;
    public float minSpawnTime = 3;
    public float maxSpawnTime = 10;
    public float VIPCustomerChance;
    public float EALCustomerChance;

    [Header ("Restocker")]
    public float deliveryTime = 5;

    [Header ("Update")]
    public int maxMultiplierPrice;
    public int ordersToIncreaseMultPrice;
    public int drinksMaxQuantityPrice;
    public int creamsMaxQuantityPrice;
    public int fruitsMaxQuantityPrice;
    public int maxLifePrice;
    public int restockCooldownPrice;
    public int VIPChancePrice;
    public int EALChancePrice;
    public int customerWaitingTimePrice;

    private void OnEnable () {
        //Change with players pref

    }

}