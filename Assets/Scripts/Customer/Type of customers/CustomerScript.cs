using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class CustomerScript : MonoBehaviour, IPoolable
{
    #region Variables
    public int id;
    public string customerType = "Normal";
    int[] givenOrder = new int[3];
    float time = 0;
    Rigidbody2D rb;
    #endregion

    #region Methods
    void CustomerWait()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            GameEvent.instance.WaitTimeout(id, customerType);
        }
    }

    void DespawnCustomer(int _id)
    {
        if (_id == id)
        {
            LeanPool.Despawn(gameObject);
        }

    }
    #endregion

    #region MonoBehavior
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        CustomerWait();
    }

    public void OnSpawn()
    {
        time = PlayerStats.instance.waitTime;
        GameEvent.instance.OnDespawnCustomer += DespawnCustomer;
    }

    public void OnDespawn()
    {
        GameEvent.instance.OnDespawnCustomer -= DespawnCustomer;
    }


    #endregion

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cup"))
        {
            givenOrder = collision.gameObject.GetComponent<CupState>().answers;
            GameEvent.instance.Compare(givenOrder, id, customerType);
        }
    }
    #endregion








}
