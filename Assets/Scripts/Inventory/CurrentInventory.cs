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

    public int drinkMQuantity=3;
    public int creamMQuantity=3;
    public int fruitMQuantity=3;
    #endregion

    #region Methods
   
    void FirstSetupInventory()
    {
        foreach (Drink drink in Drinks)
        {
            drink.MaxQuantity = drinkMQuantity;
            drink.Quantity = drinkMQuantity;
        }
        foreach (Cream cream in Creams)
        {
            cream.MaxQuantity = creamMQuantity;
            cream.Quantity = creamMQuantity;
        }
        foreach (Fruit fruit in Fruits)
        {
            fruit.MaxQuantity = fruitMQuantity;
            fruit.Quantity = fruitMQuantity;
        }

        //Set up color id
        for (int i = 1; i < 9; i++)
        {
            Drinks[i].ColorID = i;
            Creams[i].ColorID = i;
            Fruits[i].ColorID = i;
        }
    }

    void IncreaseQuantity(string _type, int _colorId, int _amount)
    {
        if(_type == "Drink")
        {
            Drinks[_colorId].Quantity += _amount;
        }else if(_type == "Cream")
        {
            Creams[_colorId].Quantity += _amount;
        }else if(_type == "Fruit")
        {
            Fruits[_colorId].Quantity += _amount;
        }
        
    }


    void DecreaseQuantity(string _type, int _colorId, int _amount) 
    {
        if (_type == "Drink")
        {
            Drinks[_colorId].Quantity -= _amount;
        }
        else if (_type == "Cream")
        {
            Creams[_colorId].Quantity -= _amount;
        }
        else if (_type == "Fruit")
        {
            Fruits[_colorId].Quantity -= _amount;
        }
    }

    void IncreaseMaxQuantity(string _type, int _amount) 
    {
        if (_type == "Drink")
        {
            drinkMQuantity += _amount;
        }
        else if (_type == "Cream")
        {
            creamMQuantity += _amount;
        }
        else if (_type == "Fruit")
        {
            fruitMQuantity += _amount;
        }
        
    }
    #endregion

    #region MonoBehaviour
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

    private void OnEnable()
    {
        FirstSetupInventory();
        GameEvent.instance.OnIncreaseQuantity += IncreaseQuantity;
        GameEvent.instance.OnDecreaseQuantity += DecreaseQuantity;
        GameEvent.instance.OnIncreaseMaxQuantity += IncreaseMaxQuantity;

    }

    private void OnDisable()
    {
        GameEvent.instance.OnIncreaseQuantity -= IncreaseQuantity;
        GameEvent.instance.OnDecreaseQuantity -= DecreaseQuantity;
        GameEvent.instance.OnIncreaseMaxQuantity -= IncreaseMaxQuantity;
    }

    private void Start()
    {

    }
    #endregion




}

