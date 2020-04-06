using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class CustomerScript : MonoBehaviour, IPoolable
{
    #region Variables
    public int id;
    int[] givenOrder = new int[3];
    Rigidbody2D rb;
    #endregion

    #region Methods
    void ReceiveCompareResult(bool _isCorrect, int _id)
    {
        if (_id == id)
        {
            if (_isCorrect)
            {
                GameEvent.instance.CorrectOrder(id);
            }
            else
            {
                GameEvent.instance.FalseOrder(id);
            }
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
    public void OnSpawn()
    {
        GameEvent.instance.OnDespawnCustomer += DespawnCustomer;
        GameEvent.instance.OnCompareResult += ReceiveCompareResult;
    }

    public void OnDespawn()
    {
        GameEvent.instance.OnDespawnCustomer -= DespawnCustomer;
        GameEvent.instance.OnCompareResult -= ReceiveCompareResult;
    }


    #endregion

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cup"))
        {
            givenOrder = collision.gameObject.GetComponent<CupState>().answers;
            GameEvent.instance.Compare(givenOrder, id);
        }
    }
    #endregion








}
