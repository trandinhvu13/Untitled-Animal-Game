using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class NormalCustomerRequest : MonoBehaviour, IRequest, IPoolable
{
    public int[] orders;
    [SerializeField]
    GameObject drink;
    [SerializeField]
    GameObject cream;
    [SerializeField]
    GameObject fruit;

    void Start()
    {
    }

    void Update()
    {
    }

    public int[] GenerateOrders()
    {
        int drink;
        int cream;
        int fruit;

        drink = Random.Range(0, 8);
        cream = Random.Range(0, 8);
        fruit = Random.Range(0, 8);

        int[] finalOrder = { drink, cream, fruit };

        return finalOrder;
    }

    public void ShowGraphic(int[] _order)
    {
        Sprite drinkSprite = CurrentInventory.instance.Drinks[_order[0]].order;
        RuntimeAnimatorController creamSprite = CurrentInventory.instance.Creams[_order[1]].order;
        Sprite fruitSprite = CurrentInventory.instance.Fruits[_order[2]].order;

        drink.GetComponent<SpriteRenderer>().sprite = drinkSprite;
        cream.GetComponent<Animator>().runtimeAnimatorController = creamSprite;
        fruit.GetComponent<SpriteRenderer>().sprite = fruitSprite;
    }

    public int[] GetOrders()
    {
        return orders;
    }

    public void OnSpawn()
    {
        orders = GenerateOrders();
        //ShowGraphic(orders);
    }

    public void OnDespawn()
    {
        Debug.Log("Got despawn");
    }
}
