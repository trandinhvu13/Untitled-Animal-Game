using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public LeanTweenType trashEaseType;
    public float trashTweenTime;
    public GameObject visual;
    private void OnTriggerEnter2D (Collider2D collision) { 
        if(collision.gameObject.CompareTag("Cup") ){
            LeanTween.scale (visual, new Vector3 (1.2f, 1.2f, 1.2f), trashTweenTime).setEase (trashEaseType);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag ("Cup")) {
            LeanTween.scale (visual, new Vector3 (1f, 1f, 1f), trashTweenTime).setEase (trashEaseType);
        }
    }
}
