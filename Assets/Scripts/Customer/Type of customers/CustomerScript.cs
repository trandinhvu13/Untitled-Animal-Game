using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using TMPro;
using UnityEngine;

public class CustomerScript : MonoBehaviour, IPoolable {
    [Header ("Components")]
    #region Components
    Rigidbody2D rb;
    public CustomerSprite customerSprite;
    public Animator ani;
    public TextMeshProUGUI timerNum;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D col;
    #endregion

    #region LeanTween
    [Header ("LeanTween Properties")]
    public float spawnAnimationTime;
    public float orderAnimationTime;
    public float despawnAnimationTime;
    public float orderDespawnAnimationTime;
    public float shakeDuration;
    public float shakeAmount;
    public float floatAmount;

    [Header ("LeanTween Ease Type")]
    public LeanTweenType spawnEaseType;
    public LeanTweenType orderEaseType;
    public LeanTweenType despawnEaseType;
    public LeanTweenType floatingEaseType;
    public LeanTweenType flickeringEaseType;
    private int hoverID;

    #endregion

    #region Variables
    [Header ("GameObject")]
    public GameObject order;
    public RectTransform timerGameObj;
    public GameObject orderTimer;
    public GameObject bubble;
    [Header ("Properties")]
    public string customerType;
    public int id;
    int[] givenOrder = new int[3];
    float time = 0;
    bool isWaiting = true;

    bool isFlickering = true;

    #endregion

    #region Methods
    void CustomerWait () {
        if (isWaiting) {
            time -= Time.deltaTime;
            if (time <= 0.5f) {
                isWaiting = false;
                time = 0;
                col.enabled = false;
                GameEvent.instance.WaitTimeout (id, customerType);

            }
        }

    }

    void Flickering () {
        void flickFaster () {
            LeanTween.alpha (bubble, 0, 0.15f).setLoopPingPong (10).setEase (floatingEaseType);
        }
        LeanTween.alpha (bubble, 0, 0.5f).setLoopPingPong (3).setEase (floatingEaseType).setOnComplete (flickFaster);
    }

    void DespawnCustomer (int _id, string _reason) {
        void despawn () {
            LeanPool.Despawn (gameObject);
        }
        void shake () {
            CameraShake.Shake (shakeDuration, shakeAmount);
        }
        
        if (_id == id) {
            if (_reason == "Correct") {

                //show particle effect

            } else if (_reason == "Timeout" || _reason == "Wrong") {
                //add red X animation

            }
            LeanTween.scale (order, new Vector3 (0f, 0f, 0), despawnAnimationTime).setEase (despawnEaseType).setOnComplete (shake);
            LeanTween.scale (timerGameObj, new Vector3 (0f, 0f, 0), despawnAnimationTime).setEase (despawnEaseType);
            LeanTween.moveY (gameObject, 0f, despawnAnimationTime).setEase (despawnEaseType).setOnComplete (despawn).setDelay (orderDespawnAnimationTime);

        }

    }

    void StartupAnimation () {
        //LeanTween.resume(hoverID);
        void hover () {
            hoverID = LeanTween.moveLocalY (orderTimer, 0.0083f, floatAmount).setEase (floatingEaseType).setLoopPingPong (-1).id;
            //LeanTween.moveLocalY (orderTimer, 0.0083f, floatAmount).setEase (floatingEaseType).setLoopPingPong (-1);
        }
        int rand = Random.Range (0, 39);
        spriteRenderer.sprite = CustomerDatabase.instance.customer[rand].sprite;
        ani.runtimeAnimatorController = CustomerDatabase.instance.customer[rand].animationController;
        LeanTween.moveLocalY (gameObject, 1.215f, spawnAnimationTime).setEase (spawnEaseType);
        LeanTween.scale (order, new Vector3 (0.11f, 0.11f, 0.11f), orderAnimationTime).setFrom (new Vector3 (0, 0, 0)).setDelay (spawnAnimationTime).setEase (orderEaseType);
        LeanTween.scale (timerGameObj, new Vector3 (1, 1, 1), orderAnimationTime).setFrom (new Vector3 (0, 0, 0)).setDelay (spawnAnimationTime).setEase (orderEaseType).setOnComplete (hover);
    }

    #endregion

    #region MonoBehavior
    void Awake () {
        rb = GetComponent<Rigidbody2D> ();
        //rb.gravityScale = 0f;
    }

    void Update () {
        timerNum.text = ((int) time).ToString ();
        CustomerWait ();
        if ((int) time == 5 && isFlickering) {
            isFlickering = false;
            Flickering ();
        }
    }

    public void OnSpawn () {
        time = PlayerStats.instance.waitTime;
        col.enabled = true;
        isWaiting = true;
        GameEvent.instance.OnDespawnCustomer += DespawnCustomer;
        gameObject.transform.localScale = new Vector3 (9.5f, 9.5f, 9.5f);
        StartupAnimation ();
        isFlickering = true;
    }

    public void OnDespawn () {
        GameEvent.instance.OnDespawnCustomer -= DespawnCustomer;
        order.transform.localScale = new Vector3 (0, 0, 0);
        timerGameObj.transform.localScale = new Vector3 (0, 0, 0);
        isFlickering = true;
        LeanTween.cancel (hoverID);
        orderTimer.transform.localPosition = new Vector3(0, -0.01f, 0);
    }

    #endregion

    #region Trigger
    private void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.CompareTag ("Cup")) {
            LeanTween.scale (gameObject, new Vector3 (11.5f, 11.5f, 11.5f), 0.2f).setEase (LeanTweenType.easeInOutQuad);
        }

    }
    private void OnTriggerExit2D (Collider2D other) {
        if (other.gameObject.CompareTag ("Cup")) {
            LeanTween.scale (gameObject, new Vector3 (9.5f, 9.5f, 9.5f), 0.2f).setEase (LeanTweenType.easeInOutQuad);
        }
    }
    #endregion

}