using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class NormalCustomerRequest : MonoBehaviour, IRequest, IPoolable
{
    #region Variables
    public int[] orders;
    [SerializeField]
    GameObject drink;
    [SerializeField]
    GameObject cream;
    [SerializeField]
    GameObject fruit;
    #endregion

    #region Methods
    public int[] GenerateOrders()
    {
        int drink;
        int cream;
        int fruit;

        drink = Random.Range(0, 7);
        cream = Random.Range(0, 7);
        fruit = Random.Range(0, 7);

        while(drink == cream || drink == fruit)
        {
            drink = Random.Range(0, 7);
        }
        while(cream == fruit)
        {
            cream = Random.Range(0, 7);
        }

        int[] finalOrder = { drink, cream, fruit };

        return finalOrder;
    }

    public void ShowGraphic(int[] _order)
    {
        Sprite drinkSprite = CurrentInventory.instance.Drinks[_order[0]].order;
        Sprite creamSprite = CurrentInventory.instance.Creams[_order[1]].order;
        Sprite fruitSprite = CurrentInventory.instance.Fruits[_order[2]].order;

        drink.GetComponent<SpriteRenderer>().sprite = drinkSprite;
        cream.GetComponent<SpriteRenderer>().sprite = creamSprite;
        fruit.GetComponent<SpriteRenderer>().sprite = fruitSprite;
    }

    public int[] GetOrders()
    {
        return orders;
    }

    #endregion

    #region MonoBehaivour
    public void OnSpawn()
    {
        orders = GenerateOrders();
        //ShowGraphic(orders);
    }

    public void OnDespawn()
    {
        Debug.Log("Got despawn");
    }
    #endregion
    

    
    
}
