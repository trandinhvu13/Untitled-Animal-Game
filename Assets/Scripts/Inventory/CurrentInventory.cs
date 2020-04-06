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
        FirstSetupInventory();
    }
    private void Start()
    {

    }

    private void ObjUse()
    {

    }

    void FirstSetupInventory()
    {
        foreach (Drink drink in Drinks)
        {
            drink.MaxQuantity = 5;
            drink.Quantity = 5;
        }
        foreach (Cream cream in Creams)
        {
            cream.MaxQuantity = 5;
            cream.Quantity = 5;
        }
        foreach (Fruit fruit in Fruits)
        {
            fruit.MaxQuantity = 5;
            fruit.Quantity = 5;
        }

        for (int i = 0; i < 9; i++)
        {
            Drinks[i].colorID = i;
            Creams[i].colorID = i;
            Fruits[i].colorID = i;
        }
    }

    void IncreaseQuantity<T>(List<T> _list, int _colorId, int _amount) where T : IInventory
    {
        _list[_colorId].Quantity += _amount;
    }


    void DecreaseQuantity<T>(T[] _list, int _colorId, int _amount) where T : IInventory
    {
        _list[_colorId].Quantity -= _amount;
    }

    void IncreaseMaxQuantity<T>(T[] _list, int _colorId, int _amount) where T : IInventory
    {
        _list[_colorId].MaxQuantity += _amount;
    }

}

