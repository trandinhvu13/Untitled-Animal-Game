using System.Collections;
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

        leanDrag.enabled = false;
        isDraggable = true;
        spriteRenderer.sprite = scriptableObject.playerInventory;
        textMeshPro.text = scriptableObject.Quantity.ToString ();
        spriteRenderer.sortingOrder = defaultSortingOrder;

    }

    private void OnDisable () {
        GameEvent.instance.OnToggleCreamCollider -= ToggleCollider;

    }
    void Start () {

    }

    void Update () {
        textMeshPro.text = scriptableObject.Quantity.ToString ();
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

        isBeingHeld = true;
        GameEvent.instance.ToggleScroll (false);
        spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        pickUpPos = rect.anchoredPosition;
        //make pickup sound
        spriteRenderer.sortingOrder = selectSortingOrder;

        LeanTween.scale (gameObject, new Vector3 (0.65f, 0.65f, 0.65f), 0.25f).setEase (LeanTweenType.easeOutQuad);

    }

    public void BeingHold () {
        // //enable leanDrag
        leanDrag.enabled = true;
    }

    public void Drop () {
        void changeBackMask () {
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }

        void resizeBig () {
            changeBackMask ();
            LeanTween.scale (gameObject, new Vector3 (0.5f, 0.5f, 0.5f), 0.25f).setEase (LeanTweenType.easeOutQuad);
        }

        void restockerMove () {
            changeBackMask ();
            rect.anchoredPosition = pickUpPos;
            LeanTween.scale (gameObject, new Vector3 (0.5f, 0.5f, 0.5f), 0.25f).setEase (LeanTweenType.easeOutQuad);
        }

        if (isBeingHeld) {
            isBeingHeld = false;

            GameEvent.instance.ToggleScroll (true);
            leanDrag.enabled = false;
            spriteRenderer.sortingOrder = defaultSortingOrder;

            //Check Collision

            if (currentCollided != null) {
                if (currentCollided.gameObject.CompareTag ("Cup")) {
                    GameEvent.instance.HandleDropItem (objType, objColorID, isDraggable);
                    LeanTween.scale (gameObject, new Vector3 (0, 0, 0), 0f);
                    LeanTween.moveLocal (gameObject, pickUpPos, 0).setOnComplete (resizeBig);

                } else if (currentCollided.gameObject.CompareTag ("Restocker")) {
                    if (scriptableObject.Quantity < scriptableObject.MaxQuantity) {
                        GameEvent.instance.RestockItem (objType, objColorID);
                        LeanTween.scale (gameObject, new Vector3 (0, 0, 0), 0.25f).setEase (LeanTweenType.easeOutQuad).setOnComplete (restockerMove);
                    } else {
                        rect.anchoredPosition = pickUpPos;
                        resizeBig ();
                    }

                } else {
                    //transform ve pick up pos(hieu ung bay lai ve cho cu)
                    LeanTween.moveLocal (gameObject, pickUpPos, 0.3f).setEase (LeanTweenType.easeOutBack).setOnComplete (resizeBig);
                }
            } else {
                LeanTween.moveLocal (gameObject, pickUpPos, 0.3f).setEase (LeanTweenType.easeOutBack).setOnComplete (resizeBig);
            }

        }

    }

    public void ToggleCollider (bool isEnabled) {
        col.enabled = isEnabled;
    }

    #endregion

}