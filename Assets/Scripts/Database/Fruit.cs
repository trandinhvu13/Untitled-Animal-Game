using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fruit", menuName = "ScriptableObjects/Fruit")]
public class Fruit : ScriptableObject
{
    public string colorName;
    public string colorID;
    public int quantity;
    public int maxQuantity;
    public Sprite playerInventory;
    public Sprite order;
    public Sprite cup;

}
