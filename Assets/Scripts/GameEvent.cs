using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvent : MonoBehaviour
{
    #region Singleton
    public static GameEvent instance = null;

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
    #endregion

    #region Customer
    public event Action<int[], int, string> OnCompare;
    public void Compare(int[] _givenOrder, int _id, string _type)
    {
        OnCompare?.Invoke(_givenOrder, _id, _type);
    }


    public event Action<int, string> OnCorrectOrder;
    public void CorrectOrder(int _id, string _type)
    {
        OnCorrectOrder?.Invoke(_id, _type);
    }


    public event Action<int, string> OnFalseOrder;
    public void FalseOrder(int _id, string _type)
    {
        OnFalseOrder?.Invoke(_id, _type);
    }

    public event Action<int> OnDespawnCustomer;

    public void DespawnCustomer(int _id)
    {
        OnDespawnCustomer?.Invoke(_id);
    }

    public event Action<int> OnRequestNextVIPOrder;

    public void RequestNextVipOrder(int _id)
    {
        OnRequestNextVIPOrder?.Invoke(_id);
    }

    public event Action<int[], int> OnReceiveNextVIPOrder;
    public void ReceiveNextVIPOrders(int[] _newOrder, int _id)
    {
        OnReceiveNextVIPOrder?.Invoke(_newOrder, _id);
    }

    public event Action<int> OnFinalVIPOrder;

    public void FinalVIPOrder(int _id)
    {
        OnFinalVIPOrder?.Invoke(_id);
    }
    #endregion

}
