using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    Transform seat1;
    [SerializeField]
    Transform seat2;
    [SerializeField]
    Transform seat3;
    [SerializeField]
    Transform seat4;

    public bool[] seatStats = { false, false, false, false };

    public float[] spawnTime = { 0, 0, 0, 0 };
    public float minSpawnTime=3;
    public float maxSpawnTime=10;
    public float[] time = { 0, 0, 0, 0 }; //Current time

    private void Start()
    {
        for (int i = 0; i < time.Length; i++)
        { SetRandomTime(i); }


    }

    private void Update()
    {
        for (int i = 0; i < time.Length; i++)
        {if(seatStats[i] == false)
            {
                time[i] += Time.deltaTime;
                if (time[i] >= spawnTime[i])
                {
                    SpawnObject(i);
                    SetRandomTime(i);
                }
            }
            
        }



    }
    void SpawnObject(int index)
    {
        time[index] = 0;
        seatStats[index] = true;
        Debug.Log("day la" + index);

    }

    void SetRandomTime(int index)
    {

        spawnTime[index] = Random.Range(minSpawnTime, maxSpawnTime);
    }
}

