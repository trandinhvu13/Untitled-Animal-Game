﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lean.Pool;
public class CustomerManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    static Transform seat1;
    [SerializeField]
    static Transform seat2;
    [SerializeField]
    static Transform seat3;
    [SerializeField]
    static Transform seat4;

    [SerializeField]
    GameObject normalCustomer;
    [SerializeField]
    GameObject vipCustomer;
    [SerializeField]
    GameObject eatALotCustomer;

    public int[][] seatsOrders = new int[4][];
    public Transform[] seatsPlaces = { seat1, seat2, seat3, seat4 };
    public bool[] seatStats = { false, false, false, false };
    public float[] spawnTimes = { 0, 0, 0, 0 };
    public float minSpawnTime = 3;
    public float maxSpawnTime = 10;
    public float[] times = { 0, 0, 0, 0 }; //Current time

    public static CustomerManager instance = null;
    #endregion


    #region Method
    void SpawnObject(int index)
    {
        times[index] = 0;
        seatStats[index] = true;
        var prefabToSpawn = new GameObject();
        bool foundObjectToPool = false;
        while (foundObjectToPool == false)
        {
            if (Random.value <= 0.1)
            {
                prefabToSpawn = vipCustomer;
                foundObjectToPool = true;
            }
            else if (Random.value <= 0.2)
            {
                prefabToSpawn = eatALotCustomer;
                foundObjectToPool = true;
            }
            else
            {
                prefabToSpawn = normalCustomer;
                foundObjectToPool = true;
            }
        }


        GameObject customer = LeanPool.Spawn(prefabToSpawn, seatsPlaces[index], false);

        IRequest orderScript = customer.GetComponent<IRequest>();
        CustomerScript customerScript = customer.GetComponent<CustomerScript>();

        customerScript.id = index;
        seatsOrders[index] = orderScript.GetOrders();
    }

    void SetRandomTime(int index)
    {

        spawnTimes[index] = Random.Range(minSpawnTime, maxSpawnTime);
    }

    public void CompareAnswers(int[] order, int id)
    {
        bool _isCorrect = false;

        if (order.SequenceEqual(seatsOrders[id]))
        {
            _isCorrect = true;
        }
        else
        {
            _isCorrect = false;
        }
        GameEvent.instance.CompareResult(_isCorrect, id);
    }

    void CorrectCustomerHandler(int _id)
    {
        seatStats[_id] = false;
        GameEvent.instance.DespawnCustomer(_id);
        //cong diem
    }

    void FalseCustomerHandler(int _id)
    {
        seatStats[_id] = false;
        GameEvent.instance.DespawnCustomer(_id);
        //tru diem
    }
    #endregion


    #region MonoBehaviour
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        //GameEvent
        GameEvent.instance.OnCorrectOrder += CorrectCustomerHandler;
        GameEvent.instance.OnCorrectOrder += FalseCustomerHandler;
        GameEvent.instance.OnCompare += CompareAnswers;
    }
    private void OnDisable()
    {
        //GameEvent
        GameEvent.instance.OnCorrectOrder -= CorrectCustomerHandler;
        GameEvent.instance.OnCorrectOrder -= FalseCustomerHandler;
        GameEvent.instance.OnCompare -= CompareAnswers;
    }

    private void Start()
    {
        for (int i = 0; i < times.Length; i++)
        { SetRandomTime(i); }

        seatsOrders[0] = new int[3];
        seatsOrders[1] = new int[3];
        seatsOrders[2] = new int[3];
        seatsOrders[3] = new int[3];


    }

    private void Update()
    {
        for (int i = 0; i < times.Length; i++)
        {
            if (seatStats[i] == false)
            {
                times[i] += Time.deltaTime;
                if (times[i] >= spawnTimes[i])
                {
                    SpawnObject(i);
                    SetRandomTime(i);
                }
            }

        }

    }
    #endregion

}

