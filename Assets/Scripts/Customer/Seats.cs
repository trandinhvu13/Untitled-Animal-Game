using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seats : MonoBehaviour
{
    public int id;
    int[] givenOrder = new int[3];
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cup"))
        {
            givenOrder = collision.gameObject.GetComponent<CupState>().answers;
            //CustomerManager.instance.Compare(givenOrder, id);
        }
    }
}
