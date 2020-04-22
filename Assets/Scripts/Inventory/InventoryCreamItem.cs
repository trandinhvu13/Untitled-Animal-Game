﻿using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using TMPro;
using UnityEngine;

public class InventoryCreamItem : MonoBehaviour {
    #region Components
    [SerializeField]
    private BoxCollider2D col;
    [SerializeField]
    private LeanDragTranslate leanDrag;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private RectTransform rect;
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    #endregion

    #region Variables
    private Collider2D currentCollided;
    private Vector2 pickUpPos;
    private bool isBeingHeld = false;
    public Cream scriptableObject;
    #endregion

    #region Monos
    private void OnEnable () {
        GameEvent.instance.OnToggleCreamCollider += ToggleCollider;
        leanDrag.enabled = false;
        spriteRenderer.sprite = scriptableObject.playerInventory;
        textMeshPro.text = scriptableObject.Quantity.ToString ();

    }

    private void OnDisable () {
        GameEvent.instance.OnToggleCreamCollider -= ToggleCollider;
    }
    void Start () {

    }

    void Update () {
        if (isBeingHeld) {
            BeingHold ();
        }
    }
    #endregion

    #region Collisions
    private void OnTriggerEnter2D (Collider2D other) {
        currentCollided = other;
    }
    private void OnTriggerExit2D (Collider2D other) {
        currentCollided = null;
    }
    #endregion

    #region Methods
    public void PickUp () {
        isBeingHeld = true;
        //send message stop scroll
        GameEvent.instance.ToggleScroll (false);
        //change mask interaction
        spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        //save pickup pos
        pickUpPos = rect.anchoredPosition;
        //make pickup sound
        textMeshPro.color = new Color32 (43, 15, 49, 0);
    }

    public void BeingHold () {
        // //enable leanDrag
        leanDrag.enabled = true;
    }

    public void Drop () {
        if (isBeingHeld) {
            isBeingHeld = false;
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
            textMeshPro.color = new Color32 (43, 15, 49, 255);
        }

    }

    public void ToggleCollider (bool isEnabled) {
        col.enabled = isEnabled;
    }

    #endregion

}