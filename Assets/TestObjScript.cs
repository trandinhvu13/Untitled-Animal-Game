using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjScript : MonoBehaviour {
    public Drink drink;
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    private Vector2 touchPos;
    private Vector2 letGoPos;

    private void OnEnable () {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
        spriteRenderer.sprite = drink.playerInventory;
    }
    void Update () {
        if (isBeingHeld) {
            this.gameObject.transform.localPosition = new Vector3 (touchPos.x - startPosX, touchPos.y - startPosY, 0);
        }
    }

    void OnTouchDown (Vector2 point) {
        isBeingHeld = true;
        touchPos = point;

        startPosX = touchPos.x - this.transform.localPosition.x;
        startPosY = touchPos.y - this.transform.localPosition.y;

    }

    void OnTouchUp (Vector2 point) {
        letGoPos = point;
        isBeingHeld = false;
    }

    void OnTouchStay (Vector2 point) {
        isBeingHeld = true;
        touchPos = point;
    }

    void OnTouchExit () {
    }

}