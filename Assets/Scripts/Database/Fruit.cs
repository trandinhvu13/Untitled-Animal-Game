using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fruit", menuName = "ScriptableObjects/Fruit")]
public class Fruit : ScriptableObject, IInventory
{
    public int colorID;
    public int Quantity { get; set; }
    public int MaxQuantity { get; set; }

    public Sprite playerInventory;
    public Sprite order;
    public Sprite cup;

    
}
