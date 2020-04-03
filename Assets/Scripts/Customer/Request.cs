using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request : MonoBehaviour
{
    public int[] order;
    [SerializeField]
    GameObject drink;
    [SerializeField]
    GameObject cream;
    [SerializeField]
    GameObject fruit;

    
    void Start()
    {
        
    }

    private void OnEnable()
    {
        order = GenerateOrder();
        ShowGraphic(order);
    }

    void Update()
    {
        
    }

    int[] GenerateOrder()
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

    void ShowGraphic(int[] _order)
    {
        Sprite drinkSprite = CurrentInventory.instance.Drink[_order[0]].order;
        RuntimeAnimatorController creamSprite = CurrentInventory.instance.Cream[_order[1]].order;
        Sprite fruitSprite = CurrentInventory.instance.Fruit[_order[2]].order;

        drink.GetComponent<SpriteRenderer>().sprite = drinkSprite;
        cream.GetComponent<Animator>().runtimeAnimatorController = creamSprite;
        fruit.GetComponent<SpriteRenderer>().sprite = fruitSprite;
    }
}
