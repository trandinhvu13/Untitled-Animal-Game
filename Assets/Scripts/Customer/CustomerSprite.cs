using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Customer", menuName = "ScriptableObjects/Customer")]
public class CustomerSprite : ScriptableObject {
    public string Name;
    public int ID;
    public Sprite sprite;
    public RuntimeAnimatorController animationController;
}