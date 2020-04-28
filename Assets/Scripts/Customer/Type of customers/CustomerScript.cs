using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class CustomerScript : MonoBehaviour, IPoolable {
    #region Components
    Rigidbody2D rb;
    public CustomerSprite customerSprite;
    public Animator ani;
    #endregion

    #region Variables
    public string customerType;

    public int id;
    int[] givenOrder = new int[3];
    float time = 0;
    public GameObject order;
    public float spawnAnimationTime;
    public float orderAnimationTime;
    public float despawnAnimationTime;
    public float orderDespawnAnimationTime;
    public LeanTweenType spawnEaseType;
    public LeanTweenType orderEaseType;
    public LeanTweenType despawnEaseType;
    public SpriteRenderer spriteRenderer;

    #endregion

    #region Methods
    void CustomerWait () {
        time -= Time.deltaTime;
        if (time <= 0) {
            time = PlayerStats.instance.waitTime;
            GameEvent.instance.WaitTimeout (id, customerType);

        }
    }

    void DespawnCustomer (int _id, string _reason) {
        void despawn () {
            LeanPool.Despawn (gameObject);
        }
        if (_id == id) {
            if (_reason == "Correct") {

            } else if (_reason == "Wrong") {

            } else if (_reason == "Timeout") {
                LeanTween.scale (order, new Vector3 (0f, 0f, 0), despawnAnimationTime).setEase (despawnEaseType);
                LeanTween.moveY (gameObject, 0f, despawnAnimationTime).setEase (despawnEaseType).setOnComplete (despawn).setDelay(orderDespawnAnimationTime);
            }

        }

    }

    void StartupAnimation () {
        int rand = Random.Range (0, 39);
        spriteRenderer.sprite = CustomerDatabase.instance.customer[rand].sprite;
        ani.runtimeAnimatorController = CustomerDatabase.instance.customer[rand].animationController;
        LeanTween.moveLocalY (gameObject, 1.215f, spawnAnimationTime).setEase (spawnEaseType);
        LeanTween.scale (order, new Vector3 (0.11f, 0.11f, 0.11f), orderAnimationTime).setFrom (new Vector3 (0, 0, 0)).setDelay (spawnAnimationTime).setEase (orderEaseType);
    }

    #endregion

    #region MonoBehavior
    void Awake () {
        rb = GetComponent<Rigidbody2D> ();
        //rb.gravityScale = 0f;
    }

    void Update () {
        CustomerWait ();
    }

    public void OnSpawn () {
        time = PlayerStats.instance.waitTime;
        GameEvent.instance.OnDespawnCustomer += DespawnCustomer;
        gameObject.transform.localScale = new Vector3 (9.5f, 9.5f, 9.5f);
        StartupAnimation ();
    }

    public void OnDespawn () {
        GameEvent.instance.OnDespawnCustomer -= DespawnCustomer;
        order.transform.localScale = new Vector3 (0, 0, 0);
        //scale to 0 -> do sprite animation
    }

    #endregion

    #region Trigger

    #endregion

}