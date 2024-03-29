﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cream", menuName = "ScriptableObjects/Cream")]
public class Cream : ScriptableObject, IInventory
{
    public int ColorID { get; set; }
    public int Quantity { get; set; }
    public int MaxQuantity { get; set; }
    public string Type { get => Type; set => Type = "Cream"; }
    public Sprite playerInventory;
    public Sprite order;
    public Sprite cup;
}
