using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class EatALotCustomerRequest : MonoBehaviour, IRequest, IPoolable
{
    #region Variables
    public int[] orders{get; set;}
    public int id;
    public Queue<int[]> queueOfOrders= new Queue<int[]>();
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

        drink = Random.Range(1, 8);
        cream = Random.Range(1, 8);
        fruit = Random.Range(1, 8);

        
        if (cream == fruit)
        {
            cream = Random.Range(1, 8);
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
        GameEvent.instance.OnRequestNextVIPOrder += GenerateNextOrders;

        numOfOrders = Random.Range(2, 4);

        for (int i = 1; i < numOfOrders; i++)
        {
            queueOfOrders.Enqueue(GenerateOrders());
        }
        orders = queueOfOrders.Peek();
        ShowGraphic(orders);

    }

    public void OnDespawn()
    {
    }
    #endregion




}
