using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cream", menuName = "ScriptableObjects/Cream")]
public class Cream : ScriptableObject, IInventory
{
    public int colorID;
    public int Quantity { get; set; }
    public int MaxQuantity { get; set; }

    public Sprite playerInventory;
    public RuntimeAnimatorController order;
    public RuntimeAnimatorController cup;
}
