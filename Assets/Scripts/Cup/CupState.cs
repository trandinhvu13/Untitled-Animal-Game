using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupState : MonoBehaviour
{
    public bool[] slotIsFull = { false, false, false };
    public int[] answers;

    void Awake()
    {
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string type = collision.GetComponent<IInventory>().Type;
        int colorID = collision.GetComponent<IInventory>().ColorID;
        if (type == "Drink")
        {
            if (slotIsFull[0] == false)
            {
                answers[0] = colorID;
                GameEvent.instance.DecreaseQuantity(type, colorID, 1);
            }
            else
            {
                //return back to slot
            };
        }
        else if (type == "Cream")
        {

            if (slotIsFull[1] == false)
            {
                answers[1] = colorID;
                GameEvent.instance.DecreaseQuantity(type, colorID, 1);
            }
            else
            {
            };
        }
        else if (type == "Fruit")
        {

            if (slotIsFull[2] == false)
            {
                answers[2] = colorID;
                GameEvent.instance.DecreaseQuantity(type, colorID, 1);
            }
            else
            {
            };
        }

    }
}
