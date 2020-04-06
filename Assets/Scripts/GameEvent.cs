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
    public event Action<int[], int> OnCompare;
    public void Compare(int[] _givenOrder, int _id)
    {
        OnCompare?.Invoke(_givenOrder, _id);
    }


    public event Action<bool, int> OnCompareResult;
    public void CompareResult(bool _iscorrect, int _id)
    {
        OnCompareResult?.Invoke(_iscorrect, _id);
    }

    public event Action<int> OnCorrectOrder;
    public void CorrectOrder(int _id)
    {
        OnCorrectOrder?.Invoke(_id);
    }

    public event Action<int> OnFalseOrder;
    public void FalseOrder(int _id)
    {
        OnFalseOrder?.Invoke(_id);
    }

    public event Action<int> OnDespawnCustomer;

    public void DespawnCustomer(int _id)
    {
        OnDespawnCustomer?.Invoke(_id);
    }
    #endregion

}
