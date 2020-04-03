using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CurrentInventory : MonoBehaviour
{

    public List<Drink> Drink = new List<Drink>();
    public List<Cream> Cream = new List<Cream>();
    public List<Fruit> Fruit = new List<Fruit>();

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
