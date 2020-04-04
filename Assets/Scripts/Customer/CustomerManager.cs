using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lean.Pool;
public class CustomerManager : MonoBehaviour
{
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
    void SpawnObject(int index)
    {
        times[index] = 0;
        seatStats[index] = true;
        GameObject prefabToSpawn = new GameObject();
        bool foundObjectToPool = false;
        while (foundObjectToPool == false)
        {
            if (Random.value <= 0.01)
            {
                prefabToSpawn = vipCustomer;
                foundObjectToPool = true;
            }
            else if (Random.value <= 0.1)
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

        IRequest customerScript = customer.GetComponent<IRequest>();
        seatsOrders[index] = customerScript.GetOrders();
        Debug.Log(seatsOrders[index]);
    }

    void SetRandomTime(int index)
    {

        spawnTimes[index] = Random.Range(minSpawnTime, maxSpawnTime);
    }
}

