using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class VIPCustomerRequest : MonoBehaviour, IRequest, IPoolable
{
    #region Variables
    public int[] orders;
    public int id;
    public Queue<int[]> queueOfOrders;
    public int numOfOrders;
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

        while (drink == cream || drink == fruit)
        {
            drink = Random.Range(0, 7);
        }
        while (cream == fruit)
        {
            cream = Random.Range(0, 7);
        }

        int[] finalOrder = { drink, cream, fruit };

        return finalOrder;
    }

    public void GenerateNextOrders(int _id)
    {
        if (_id == id)
        {
            if (numOfOrders > 0)
            {
                queueOfOrders.Dequeue();
                orders = queueOfOrders.Peek();
                //show next graphic
                GameEvent.instance.ReceiveNextVIPOrders(orders, id);
            }
            else
            {
                GameEvent.instance.FinalVIPOrder(id);
            }

            numOfOrders--;
        }

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

    #endregion

    #region MonoBehaivour
    public void OnSpawn()
    {
        GameEvent.instance.OnRequestNextVIPOrder += GenerateNextOrders;

        numOfOrders = Random.Range(2, 4);

        for (int i = 1; i < numOfOrders; i++)
        {
            queueOfOrders.Enqueue(GenerateOrders());
        }
        orders = queueOfOrders.Peek();
        //show graphic

    }

    public void OnDespawn()
    {
        Debug.Log("Got despawn");
    }
    #endregion




}
