using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class VIPCustomerRequest : MonoBehaviour, IRequest, IPoolable {
    #region Variables
    public int[] orders { get; set; }
    public int id;
    public int numOfOrders;
    [SerializeField]
    GameObject drink;
    [SerializeField]
    GameObject cream;
    [SerializeField]
    GameObject fruit;
    public GameObject orderGameObj;
    public LeanTweenType changeOrderAnimation;
    public float animationTime;
    #endregion

    #region Methods
    public int[] GenerateOrders () {

        int drink;
        int cream;
        int fruit;

        drink = Random.Range (1, 8);
        cream = Random.Range (1, 8);
        fruit = Random.Range (1, 8);

        if (cream == fruit) {
            cream = Random.Range (1, 8);
        }

        int[] finalOrder = { drink, cream, fruit };

        return finalOrder;
    }

    public void HandleNextOrder (int _id) {
        if (_id == id) {
            numOfOrders--;
            
            if (numOfOrders > 0) {
                LeanTween.scale (orderGameObj, new Vector3 (0, 0, 0), animationTime).setFrom (gameObject.transform.localScale).setDelay (0.5f).setEase (changeOrderAnimation).setOnComplete (GenerateNextOrders);
            } else {
                GameEvent.instance.FinalVIPOrder (id);
            }
        }
    }

    public void GenerateNextOrders () {
        orders = GenerateOrders ();
        ShowGraphic(orders);
        LeanTween.scale (orderGameObj, new Vector3 (0.11f, 0.11f, 0.11f), 0.35f).setFrom (new Vector3 (0, 0, 0)).setEase (LeanTweenType.easeOutBack);
    }



public void ShowGraphic (int[] _order) {
    Sprite drinkSprite = CurrentInventory.instance.Drinks[_order[0]].order;
    Sprite creamSprite = CurrentInventory.instance.Creams[_order[1]].order;
    Sprite fruitSprite = CurrentInventory.instance.Fruits[_order[2]].order;

    drink.GetComponent<SpriteRenderer> ().sprite = drinkSprite;
    cream.GetComponent<SpriteRenderer> ().sprite = creamSprite;
    fruit.GetComponent<SpriteRenderer> ().sprite = fruitSprite;
}

public int[] GetOrders () {
    return orders;
}

#endregion

#region MonoBehaivour
public void OnSpawn () {
    GameEvent.instance.OnRequestNextVIPOrder += HandleNextOrder;

    numOfOrders = Random.Range (2, 4);
    orders = GenerateOrders ();
    ShowGraphic (orders);

}

public void OnDespawn () {
    GameEvent.instance.OnRequestNextVIPOrder -= HandleNextOrder;
}
#endregion

}