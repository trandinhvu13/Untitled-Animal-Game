﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "ScriptableObjects/Drink")]
public class Drink : ScriptableObject
{
    public string colorName;
    public string colorID;
    public int quantity;
    public int maxQuantity;
    public Sprite playerInventory;
    public Sprite order;
    public Sprite cup;

}
