using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    //Score
    public int totalScore;
    public int maxMultiplier;
    public int maxLife;

    //Invetory
    public int drinkMQuantity = 3;
    public int creamMQuantity = 3;
    public int fruitMQuantity = 3;

    //Customer
    public float waitTime;
    public float customerWaitingTime = 10;
    public float minSpawnTime = 3;
    public float maxSpawnTime = 10;
    public float VIPCustomerChance;
    public float EALCustomerChance;

    //Restocker
    public float cooldownTime;
    public float deliveryTime;

    //Upgrade
    public int maxMultiplierPrice;
    public int drinksMaxQuantityPrice;
    public int creamsMaxQuantityPrice;
    public int fruitsMaxQuantityPrice;
    public int maxLifePrice;
    public int restockTimePrice;
    public int restockCooldownPrice;
    public int VIPChancePrice;
    public int EALChancePrice;
    public int customerWaitingTimePrice;


    private void OnEnable()
    {
        //Setup
    }

}
