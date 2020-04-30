using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Restocker : MonoBehaviour {

    float deliveryTime;
    float time;
    [SerializeField]
    private TextMeshProUGUI timerUI;
    [SerializeField]
    private Collider2D col;

    bool available = true;

    public LeanTweenType itemEaseType;

    public float itemTweenTime;
    public GameObject visual;
    


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

    private void OnTriggerEnter2D (Collider2D collision) { 
        if(collision.gameObject.CompareTag("Fruit") || collision.gameObject.CompareTag("Cream")||collision.gameObject.CompareTag("Drink")){
            LeanTween.scale (visual, new Vector3 (1.2f, 1.2f, 1.2f), itemTweenTime).setEase (itemEaseType);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag ("Fruit") || other.gameObject.CompareTag ("Drink") || other.gameObject.CompareTag ("Cream")) {
            LeanTween.scale (visual, new Vector3 (1f, 1f, 1f), itemTweenTime).setEase (itemEaseType);
        }
    }
    void HandleItemDrop (string _type, int _colorID) {
        col.enabled = false;
        available = false;
        StartCoroutine (IncreaseQuantity (_type, _colorID));
    }

    IEnumerator IncreaseQuantity (string _type, int _colorId) {
        yield return new WaitForSeconds (PlayerStats.instance.deliveryTime);
        GameEvent.instance.IncreaseQuantityToMax (_type, _colorId);
        GameEvent.instance.UpdateItemUI(_type, _colorId);
        available = true;
        col.enabled = true;
    }
}