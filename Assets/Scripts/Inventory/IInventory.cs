using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory 
{
    int Quantity { get; set; }
    int MaxQuantity { get; set; }
    string Type { get; set; }
    int ColorID { get; set; }



}
