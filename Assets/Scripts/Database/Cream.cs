using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cream", menuName = "ScriptableObjects/Cream")]
public class Cream : ScriptableObject
{
    public string colorName;
    public string colorID;
    public int quantity;
    public int maxQuantity;
    public Sprite playerInventory;
    public Sprite order;
    public Sprite cup;
}
