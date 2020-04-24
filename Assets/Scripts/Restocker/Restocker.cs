using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Restocker : MonoBehaviour {

    float deliveryTime;
    float time;
    [SerializeField]
    private TextMeshProUGUI timerUI;

    bool available = true;
    private void OnEnable () {
        GameEvent.instance.OnRestockItem += HandleItemDrop;
        deliveryTime = PlayerStats.instance.deliveryTime;
        time = deliveryTime;
    }

    private void OnDisable () {
        GameEvent.instance.OnRestockItem -= HandleItemDrop;
    }
    void Start () {

    }

    void Update () {
        if (!available) {
            timerUI.text = ((int) time).ToString ();
            if (time > 0) {
                time -= Time.deltaTime;
            } else {
                available = true;
                time = deliveryTime;
            }

        } else {
            timerUI.text = "+";
        }
    }

    private void OnTriggerEnter2D (Collider2D collision) { }
    void HandleItemDrop (string _type, int _colorID) {
        //start timer
        available = false;
        StartCoroutine (IncreaseQuantity (_type, _colorID));
    }

    IEnumerator IncreaseQuantity (string _type, int _colorId) {
        yield return new WaitForSeconds (PlayerStats.instance.deliveryTime);
        GameEvent.instance.IncreaseQuantityToMax (_type, _colorId);
        GameEvent.instance.UpdateItemUI(_type, _colorId);
        available = true;
    }
}