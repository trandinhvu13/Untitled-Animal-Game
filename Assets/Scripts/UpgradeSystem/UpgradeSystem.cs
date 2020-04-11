using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour {
    public int maxMultiplierPriceStep;
    public int drinksMaxQuantityPriceStep;
    public int creamsMaxQuantityPriceStep;
    public int fruitsMaxQuantityPriceStep;
    public int maxLifePrice;
    public int restockTimePriceStep;
    public int restockCooldownPriceStep;
    public int VIPChancePriceStep;
    public int EALChancePriceStep;
    public int customerWaitingTimePriceStep;

    void UpgradeMaxMulti () {
        PlayerStats.instance.maxMultiplier += 1;
        PlayerStats.instance.totalScore -= PlayerStats.instance.maxMultiplierPrice;
        PlayerStats.instance.maxMultiplierPrice += maxMultiplierPriceStep;
    }
    void UgradeDrinksMaxQuan () {
        PlayerStats.instance.drinkMQuantity += 1;
        PlayerStats.instance.totalScore -= PlayerStats.instance.drinksMaxQuantityPrice;
        PlayerStats.instance.drinksMaxQuantityPrice += drinksMaxQuantityPriceStep;
    }
    void UpgradeCreamsMaxQuan () {
        PlayerStats.instance.creamMQuantity += 1;
        PlayerStats.instance.totalScore -= PlayerStats.instance.creamsMaxQuantityPrice;
        PlayerStats.instance.creamsMaxQuantityPrice += creamsMaxQuantityPriceStep;
    }
    void UpgradeFruitsMaxQuan () {
        PlayerStats.instance.fruitMQuantity += 1;
        PlayerStats.instance.totalScore -= PlayerStats.instance.fruitsMaxQuantityPrice;
        PlayerStats.instance.fruitsMaxQuantityPrice += fruitsMaxQuantityPriceStep;
    }

    void UpgradeMaxLife () {
        PlayerStats.instance.maxLife += 1;
        PlayerStats.instance.totalScore -= PlayerStats.instance.maxLifePrice;
        PlayerStats.instance.maxLifePrice += maxMultiplierPriceStep;
    }

    void UpgradeRestockTime () {
        PlayerStats.instance.deliveryTime -= 1;
        PlayerStats.instance.totalScore -= PlayerStats.instance.restockTimePrice;
        PlayerStats.instance.restockCooldownPrice += restockCooldownPriceStep;
    }

    void UpgradeCooldownRestockTime () {
        PlayerStats.instance.cooldownTime -= 1;
        PlayerStats.instance.totalScore -= PlayerStats.instance.restockCooldownPrice;
        PlayerStats.instance.restockCooldownPrice += restockCooldownPriceStep;
    }

    void UpgradeChanceVIP () {
        PlayerStats.instance.VIPCustomerChance += 0.02f;
        PlayerStats.instance.totalScore -= PlayerStats.instance.VIPChancePrice;
        PlayerStats.instance.VIPChancePrice += VIPChancePriceStep;
    }

    void UpgradeChanceEAL () {
        PlayerStats.instance.EALCustomerChance += 0.02f;
        PlayerStats.instance.totalScore -= PlayerStats.instance.EALChancePrice;
        PlayerStats.instance.EALChancePrice += EALChancePriceStep;
    }

    void UpgradeCustomerWaitingTime () {
        PlayerStats.instance.customerWaitingTime += 1;
        PlayerStats.instance.totalScore -= PlayerStats.instance.customerWaitingTimePrice;
        PlayerStats.instance.customerWaitingTimePrice += customerWaitingTimePriceStep;
    }


}