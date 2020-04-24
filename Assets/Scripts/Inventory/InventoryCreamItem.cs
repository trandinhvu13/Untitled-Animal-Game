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
    public bool isDraggable;
    private int defaultSortingOrder = 10;
    private int selectSortingOrder = 105;
    private string objType = "Cream";
    private int objColorID;
    #endregion

    #region Monos
    private void Awake () {
        objColorID = scriptableObject.ColorID;
    }
    private void OnEnable () {
        GameEvent.instance.OnToggleCreamCollider += ToggleCollider;
        GameEvent.instance.OnUpdateItemUI += UpdateUI;

        leanDrag.enabled = false;
        isDraggable = true;
        spriteRenderer.sprite = scriptableObject.playerInventory;
        textMeshPro.text = scriptableObject.Quantity.ToString ();
        spriteRenderer.sortingOrder = defaultSortingOrder;

    }

    private void OnDisable () {
        GameEvent.instance.OnToggleCreamCollider -= ToggleCollider;
        GameEvent.instance.OnUpdateItemUI -= UpdateUI;
    }
    void Start () {

    }

    void Update () {
        if (scriptableObject.Quantity > 0) {
            isDraggable = true;
        } else {
            isDraggable = false;
        }

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
        if (isDraggable) {
            isBeingHeld = true;
            //send message stop scroll
            GameEvent.instance.ToggleScroll (false);
            //change mask interaction
            spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
            //save pickup pos
            pickUpPos = rect.anchoredPosition;
            //make pickup sound
            //textMeshPro.color = new Color32 (43, 15, 49, 0);
            spriteRenderer.sortingOrder = selectSortingOrder;

        }

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

                    GameEvent.instance.HandleDropItem (objType, objColorID);
                    //transform ve pick up pos(hieu ung poof)

                    rect.anchoredPosition = pickUpPos;

                } else if (currentCollided.gameObject.CompareTag ("Restocker")) {
                    if (scriptableObject.Quantity < scriptableObject.MaxQuantity) {
                        GameEvent.instance.RestockItem (objType, objColorID);
                    }
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
            //textMeshPro.color = new Color32 (43, 15, 49, 255);
            spriteRenderer.sortingOrder = defaultSortingOrder;
            textMeshPro.text = scriptableObject.Quantity.ToString ();
        }

    }

    public void ToggleCollider (bool isEnabled) {
        col.enabled = isEnabled;
    }

    public void UpdateUI (string _type, int _colorID) {
        if (_type == objType && _colorID == objColorID) {
            textMeshPro.text = scriptableObject.Quantity.ToString ();
        }
    }
    #endregion

}