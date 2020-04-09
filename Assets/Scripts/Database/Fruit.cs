using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fruit", menuName = "ScriptableObjects/Fruit")]
public class Fruit : ScriptableObject, IInventory
{
    public int ColorID { get; set; }
    public int Quantity { get; set; }
    public int MaxQuantity { get; set; }
    public string Type { get => Type; set => Type = "Fruit"; }
    public Sprite playerInventory;
    public Sprite order;
    public Sprite cup;

    
}
