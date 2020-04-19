using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class InventoryItem : MonoBehaviour {
    private Collider2D currentCollided;
    [SerializeField]
    private BoxCollider2D col;
    [SerializeField]
    private LeanDragTranslate leanDrag;
    private Transform pickUpPos;
    private void OnEnable () {
        leanDrag.enabled = false;

    }
    void Start () {

    }

    void Update () {

    }
    void OnPickUp () {
        //send message stop scroll
        //change mask interaction
        //save pickup pos
        //make pickup sound
    }
    void BeingHold () {
        //enable leanDrag

    }

    void BeingDrop () {
        if (currentCollided.gameObject.CompareTag ("Cup")) {
            //neu con quantity:
            //sendmessage drop vao cup (tru quantity, them answer + sprite vao cup, tru UI)
            //transform ve pick up pos(hieu ung poof)
            //neu het quantity:
            //transform ve pick up pos (dot ngot transform)

        } else if (currentCollided.gameObject.CompareTag ("Restocker")) {
            //sendmessage drop vao restocker (restock ingredient)
            //transform ve pick up pos (co hieu ung poof)

        } else {
            //transform ve pick up pos(hieu ung poof)
        }

        //enable scroll
        //disable leanDrag
    }

    private void OnTriggerEnter2D (Collider2D other) {
        currentCollided = other;
    }
    private void OnTriggerExit2D (Collider2D other) {
        currentCollided = null;
    }

}