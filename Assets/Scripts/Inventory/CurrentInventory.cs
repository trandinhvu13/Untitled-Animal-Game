using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CurrentInventory : MonoBehaviour
{

    public List<Drink> Drinks = new List<Drink>();
    public List<Cream> Creams = new List<Cream>();
    public List<Fruit> Fruits = new List<Fruit>();

    public static CurrentInventory instance = null;
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
    private void Start()
    {
       
    }

    private void ObjUse()
    {
        
    }
}
