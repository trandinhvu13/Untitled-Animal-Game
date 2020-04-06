using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CurrentInventory : MonoBehaviour
{
    #region Variables
    public static CurrentInventory instance = null;

    public List<Drink> Drinks = new List<Drink>();
    public List<Cream> Creams = new List<Cream>();
    public List<Fruit> Fruits = new List<Fruit>();
    #endregion


    
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
