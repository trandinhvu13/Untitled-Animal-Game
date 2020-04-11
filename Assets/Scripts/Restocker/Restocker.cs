using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restocker : MonoBehaviour
{
    float time;
    
    bool available = true;
    void Start()
    {
        time = 0;
    }

    void Update()
    {
        if (time <= 0)
        {
            available = true;
        }
        else
        {
            time -= Time.deltaTime;
            available = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ingredient"))
        {
            if (available)
            {
                time = PlayerStats.instance.cooldownTime;
                string type = collision.GetComponent<IInventory>().Type;
                int colorId = collision.GetComponent<IInventory>().ColorID;
                int maxQuantity = collision.GetComponent<IInventory>().MaxQuantity;
                int quantity = collision.GetComponent<IInventory>().Quantity;
                StartCoroutine(IncreaseQuantity(type, colorId, maxQuantity, quantity));
            }
            else return;


        }
        else return;

    }

    IEnumerator IncreaseQuantity(string _type, int _colorId, int _maxQuantity, int _quantity)
    {
        yield return new WaitForSeconds(PlayerStats.instance.deliveryTime);
        GameEvent.instance.IncreaseQuantity(_type, _colorId, _maxQuantity - _quantity);
    }
}
