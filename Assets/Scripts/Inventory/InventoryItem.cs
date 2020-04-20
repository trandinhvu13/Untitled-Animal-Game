﻿using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class InventoryItem : MonoBehaviour {
    private Collider2D currentCollided;
    [SerializeField]
    private BoxCollider2D col;
    [SerializeField]
    private LeanDragTranslate leanDrag;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private Vector2 pickUpPos;
    [SerializeField]
    private RectTransform rect;
    [SerializeField]
    private bool isBeingHeld = false;
    private void OnEnable () {
        leanDrag.enabled = false;

    }

    private void OnDisable () {

    }
    void Start () {

    }

    void Update () {
        if (isBeingHeld) {
            BeingHold ();
        }
    }
    public void PickUp () {
        isBeingHeld = true;
        //send message stop scroll
        GameEvent.instance.ToggleScroll (false);
        //change mask interaction
        spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        //save pickup pos
        pickUpPos = rect.anchoredPosition;
        //make pickup sound
        Debug.Log ("pick");
    }

    public void BeingHold () {
        // //enable leanDrag
        leanDrag.enabled = true;
        Debug.Log ("hold");
    }

    public void Drop () {
        if (isBeingHeld) {
            isBeingHeld = false;
            Debug.Log ("drop");
            if (currentCollided != null) {
                if (currentCollided.gameObject.CompareTag ("Cup")) {
                    //neu con quantity:
                    //sendmessage drop vao cup (tru quantity, them answer + sprite vao cup, tru UI)
                    //transform ve pick up pos(hieu ung poof)
                    //neu het quantity:
                    //transform ve pick up pos (poof)
                    rect.anchoredPosition = pickUpPos;

                } else if (currentCollided.gameObject.CompareTag ("Restocker")) {
                    //sendmessage drop vao restocker (restock ingredient)
                    //transform ve pick up pos (poof)
                    rect.anchoredPosition = pickUpPos;

                } else {
                    //transform ve pick up pos(hieu ung bay lai ve cho cu)
                    rect.anchoredPosition = pickUpPos;
                }
            } else {
                rect.anchoredPosition = pickUpPos;
            }
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            GameEvent.instance.ToggleScroll (true);
            leanDrag.enabled = false;

        }

    }
   
    private void OnTriggerEnter2D (Collider2D other) {
        currentCollided = other;
    }
    private void OnTriggerExit2D (Collider2D other) {
        currentCollided = null;
    }

}