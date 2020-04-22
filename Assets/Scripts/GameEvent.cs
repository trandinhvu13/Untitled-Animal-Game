﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour {
    #region Singleton
    public static GameEvent instance = null;

    private void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }
    #endregion

    #region Customer
    public event Action<int[], int, string> OnCompare;
    public void Compare (int[] _givenOrder, int _id, string _type) {
        OnCompare?.Invoke (_givenOrder, _id, _type);
    }

    public event Action<int, string> OnCorrectOrder;
    public void CorrectOrder (int _id, string _type) {
        OnCorrectOrder?.Invoke (_id, _type);
    }

    public event Action<int, string> OnFalseOrder;
    public void FalseOrder (int _id, string _type) {
        OnFalseOrder?.Invoke (_id, _type);
    }

    public event Action<int> OnDespawnCustomer;

    public void DespawnCustomer (int _id) {
        OnDespawnCustomer?.Invoke (_id);
    }

    public event Action<int> OnRequestNextVIPOrder;

    public void RequestNextVipOrder (int _id) {
        OnRequestNextVIPOrder?.Invoke (_id);
    }

    public event Action<int[], int> OnReceiveNextVIPOrder;
    public void ReceiveNextVIPOrders (int[] _newOrder, int _id) {
        OnReceiveNextVIPOrder?.Invoke (_newOrder, _id);
    }

    public event Action<int> OnFinalVIPOrder;

    public void FinalVIPOrder (int _id) {
        OnFinalVIPOrder?.Invoke (_id);
    }

    public event Action<int, string> OnWaitTimeout;
    public void WaitTimeout (int _id, string _type) {
        OnWaitTimeout?.Invoke (_id, _type);
    }
    #endregion

    #region Inventory
    public event Action<string, int, int> OnDecreaseQuantity;
    public void DecreaseQuantity (string _type, int _colorId, int _amount) {
        OnDecreaseQuantity?.Invoke (_type, _colorId, _amount);
    }

    public event Action<string, int, int> OnIncreaseQuantity;
    public void IncreaseQuantity (string _type, int _colorId, int _amount) {
        OnIncreaseQuantity?.Invoke (_type, _colorId, _amount);
    }

    public event Action<string, int> OnIncreaseMaxQuantity;
    public void IncreaseMaxQuantity (string _type, int _amount) {
        OnIncreaseMaxQuantity?.Invoke (_type, _amount);
    }

    public event Action<bool> OnToggleScroll;
    public void ToggleScroll (bool isEnabled) {
        OnToggleScroll?.Invoke (isEnabled);
    }

    public event Action<bool> OnToggleFruitCollider;
    public void ToggleFruitCollider (bool isEnabled) {
        OnToggleFruitCollider?.Invoke (isEnabled);
    }

    public event Action<bool> OnToggleCreamCollider;
    public void ToggleCreamCollider (bool isEnabled) {
        OnToggleCreamCollider?.Invoke (isEnabled);
    }

    public event Action<bool> OnToggleDrinkCollider;
    public void ToggleDrinkCollider (bool isEnabled) {
        OnToggleDrinkCollider?.Invoke (isEnabled);
    }
    #endregion

    #region Score
    public event Action<int> OnIncreaseScore;
    public void IncreaseScore (int _amount) {
        OnIncreaseScore?.Invoke (_amount);
    }

    public event Action<int> OnDecreaseScore;
    public void DecreaseScore (int _amount) {
        OnDecreaseScore?.Invoke (_amount);
    }

    public event Action<int> OnChangeMultiplier;
    public void ChangeMultiplier (int _amount) {
        OnChangeMultiplier?.Invoke (_amount);
    }
    #endregion

    #region Life
    public event Action<int> OnChangeLife;
    public void ChangeLife (int _amount) {
        OnChangeLife?.Invoke (_amount);
    }

    public event Action<int> OnChangeMaxLife;
    public void ChangeMaxLife (int _amount) {
        OnChangeMaxLife?.Invoke (_amount);
    }
    #endregion

}