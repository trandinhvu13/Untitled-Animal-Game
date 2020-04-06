using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRequest 
{
    
    int[] GenerateOrders();

    void ShowGraphic(int[] _order);

    int[] GetOrders();
}
