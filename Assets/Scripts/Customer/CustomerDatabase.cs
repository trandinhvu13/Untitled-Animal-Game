using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDatabase : MonoBehaviour {
    #region Variables
    public static CustomerDatabase instance = null;
    public List<CustomerSprite> customer = new List<CustomerSprite> ();
    #endregion

    #region Monos
    private void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }

    }
    private void OnEnable () {
        for (int i = 0; i < 40; i++) {
            customer[i].ID = i;
        }
    }
    #endregion

    #region Methods

    #endregion
}