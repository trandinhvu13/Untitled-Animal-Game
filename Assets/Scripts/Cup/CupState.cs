using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupState : MonoBehaviour {
    #region Components
    #endregion

    #region Variables
    [Header ("Variables")]
    public bool[] slotIsFull = { false, false, false };
    public int[] answers = { 0, 0, 0 };
    [SerializeField]
    private SpriteRenderer fruit;
    [SerializeField]
    private SpriteRenderer cream;

    private Collider2D currentCollided;
    private bool isBeingHeld = false;
    public Vector3 defaultActivePos;
    public int cupID;
    private Vector3 pickupPos;
    public GameObject trash;

    [Header ("Sprite")]
    [SerializeField]
    private SpriteRenderer drink;
    [SerializeField]
    private string defaultSortingLayer;
    [SerializeField]
    private string pickUpSortingLayer;
    public SpriteRenderer[] sprites = new SpriteRenderer[4];

    [Header ("LeanTween")]
    public LeanTweenType easeType;
    public float tweenTime;
    public LeanTweenType itemEaseType;
    public float itemTweenTime;
    public LeanTweenType trashEaseType;
    public float trashTweenTime;
    #endregion

    #region Monos
    void Awake () { }
    private void OnEnable () {
        SetUp ();
        GameEvent.instance.OnResizeAfterDrop += ResizeAfterItemDrop;
        GameEvent.instance.OnHandleDropItem += HandleDropItem;

    }

    private void OnDisable () {
        GameEvent.instance.OnResizeAfterDrop -= ResizeAfterItemDrop;
        GameEvent.instance.OnHandleDropItem -= HandleDropItem;

    }

    void Update () {
        if (isBeingHeld) {
            Hold ();
        }
    }

    #endregion

    #region Collisions
    private void OnTriggerEnter2D (Collider2D other) {
        currentCollided = other;
        if ((other.gameObject.CompareTag ("Fruit") || other.gameObject.CompareTag ("Drink") || other.gameObject.CompareTag ("Cream")) && isBeingHeld == false && other.gameObject.GetComponent<IInventoryItem>().isBeingHeld == true) {
            LeanTween.scale (gameObject, new Vector3 (0.23f, 0.23f, 0.23f), itemTweenTime).setEase (itemEaseType);
        }
    }
    private void OnTriggerExit2D (Collider2D other) {

        if (other.gameObject.CompareTag ("Cup") || other.gameObject.CompareTag ("Trash")) {
            currentCollided = null;
        }

        if (other.gameObject.CompareTag ("Fruit") || other.gameObject.CompareTag ("Drink") || other.gameObject.CompareTag ("Cream") && isBeingHeld == false && other.gameObject.GetComponent<IInventoryItem>().isBeingHeld == true) {
            LeanTween.scale (gameObject, new Vector3 (0.2f, 0.2f, 0.2f), itemTweenTime).setEase (itemEaseType);
        }
    }
    #endregion

    #region Methods

    void ResizeAfterItemDrop () {
        LeanTween.scale (gameObject, new Vector3 (0.2f, 0.2f, 0.2f), itemTweenTime).setEase (itemEaseType);
    }
    void SetUp () {
        gameObject.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
        fruit.sprite = null;
        cream.sprite = null;
        drink.sprite = null;

        for (int i = 0; i < 3; i++) {
            slotIsFull[i] = false;
        }

        for (int i = 0; i < 3; i++) {
            answers[i] = 0;
        }
    }

    private void HandleDropItem (string _type, int _colorID, bool _isDraggable) {
        if (_isDraggable) {
            if (_type == "Fruit") {
                if (slotIsFull[2] == false) {
                    slotIsFull[2] = true;
                    answers[2] = _colorID;
                    fruit.sprite = CurrentInventory.instance.Fruits[_colorID].cup;
                    GameEvent.instance.DecreaseQuantity ("Fruit", _colorID, 1);
                } else {
                    return;
                };
            } else if (_type == "Cream") {
                if (slotIsFull[1] == false) {
                    slotIsFull[1] = true;
                    answers[1] = _colorID;
                    cream.sprite = CurrentInventory.instance.Creams[_colorID].cup;
                    GameEvent.instance.DecreaseQuantity ("Cream", _colorID, 1);
                } else {
                    return;
                };
            } else if (_type == "Drink") {
                if (slotIsFull[0] == false) {
                    slotIsFull[0] = true;
                    answers[0] = _colorID;
                    drink.sprite = CurrentInventory.instance.Drinks[_colorID].cup;
                    GameEvent.instance.DecreaseQuantity ("Drink", _colorID, 1);
                } else {
                    return;
                }
            }
        } else {
            return;
        }
    }

    private void changeSpriteOrder (string _sortingLayer) {
        for (int i = 0; i < 4; i++) {
            sprites[i].sortingLayerName = _sortingLayer;
        }
    }

    public void PickUp () {
        isBeingHeld = true;
        pickupPos = gameObject.transform.position;
        changeSpriteOrder (pickUpSortingLayer);
    }

    public void Hold () {

    }

    public void Drop () {

        void handle () {
            GameEvent.instance.HandleCup (cupID, false);
        }

        if (isBeingHeld) {
            isBeingHeld = false;
            //Check Collision
            if (currentCollided != null) {

                if (currentCollided.gameObject.CompareTag ("Customer")) {
                    LeanTween.scale (currentCollided.gameObject, new Vector3 (10, 10, 10), 0.2f).setEase (LeanTweenType.easeInOutQuad);
                } else if (currentCollided.gameObject.CompareTag ("Trash")) {
                    LeanTween.scale (trash, new Vector3 (1, 1, 1), 0.2f).setEase (LeanTweenType.easeInOutQuad);
                }

                if (currentCollided.gameObject.CompareTag ("Trash")) {
                    changeSpriteOrder (defaultSortingLayer);
                    LeanTween.scale (gameObject, new Vector3 (0, 0, 0), trashTweenTime).setEase (trashEaseType).setOnComplete (handle);
                } else if (currentCollided.gameObject.CompareTag ("Customer")) {
                    changeSpriteOrder (defaultSortingLayer);
                    int id = currentCollided.gameObject.GetComponent<CustomerScript> ().id;
                    string customerType = currentCollided.gameObject.GetComponent<CustomerScript> ().customerType;
                    GameEvent.instance.Compare (answers, id, customerType);
                    LeanTween.scale (gameObject, new Vector3 (0, 0, 0), trashTweenTime).setEase (trashEaseType).setOnComplete (handle);
                } else {
                    StartCoroutine (DropSpriteChange ());
                    LeanTween.move (this.gameObject, pickupPos, tweenTime).setEase (easeType);
                }

            } else {
                StartCoroutine (DropSpriteChange ());
                LeanTween.move (this.gameObject, pickupPos, tweenTime).setEase (easeType);
            }

        }

    }
    IEnumerator DropSpriteChange () {
        yield return new WaitForSeconds (0.4f);
        changeSpriteOrder (defaultSortingLayer);
    }

    #endregion

}