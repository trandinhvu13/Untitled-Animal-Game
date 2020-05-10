using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using UnityEngine;
public class CustomerManager : MonoBehaviour {
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
    public float[] times = { 0, 0, 0, 0 }; //Current time

    public static CustomerManager instance = null;
    #endregion

    #region Method
    void SpawnObject (int index) {
        times[index] = 0;
        seatStats[index] = true;
        var prefabToSpawn = new GameObject ();
        bool foundObjectToPool = false;
        while (foundObjectToPool == false) {
            if (Random.value <= PlayerStats.instance.VIPCustomerChance) {
                prefabToSpawn = vipCustomer;
                foundObjectToPool = true;
            } else if (Random.value <= PlayerStats.instance.EALCustomerChance) {
                prefabToSpawn = eatALotCustomer;
                foundObjectToPool = true;
            } else {
                prefabToSpawn = normalCustomer;
                foundObjectToPool = true;
            }
        }

        GameObject customer = LeanPool.Spawn (prefabToSpawn, seatsPlaces[index], false);

        IRequest orderScript = customer.GetComponent<IRequest> ();
        CustomerScript customerScript = customer.GetComponent<CustomerScript> ();

        customerScript.id = index;

        seatsOrders[index] = orderScript.orders;
    }

    void SetRandomTime (int index) {

        spawnTimes[index] = Random.Range (PlayerStats.instance.minSpawnTime, PlayerStats.instance.maxSpawnTime);
    }

    public void CompareAnswersHandler (int[] order, int id, string type) {
        if (order.SequenceEqual (seatsOrders[id])) {
            CorrectCustomerHandler (id, type);
            Debug.Log ("True");
        } else {
            FalseCustomerHandler (id, type);
            Debug.Log ("False");
        }
    }

    void CorrectCustomerHandler (int _id, string _type) {
        if (_type == "Normal") {
            seatStats[_id] = false;
            GameEvent.instance.DespawnCustomer (_id, "Correct");
            GameEvent.instance.IncreaseScore (10);
        } else if (_type == "VIP") {
            GameEvent.instance.RequestNextVipOrder (_id);
        } else if (_type == "EatALot") {
            GameEvent.instance.CorrectEALOrder (_id);
        }

    }

    void FalseCustomerHandler (int _id, string _type) {
        if (_type == "Normal") {
            seatStats[_id] = false;
            GameEvent.instance.DecreaseScore (20);
        } else if (_type == "VIP") {
            seatStats[_id] = false;
            GameEvent.instance.DecreaseScore (100);
        } else if (_type == "EatALot") {
            seatStats[_id] = false;

            GameEvent.instance.DecreaseScore (40);
        }
        GameEvent.instance.DespawnCustomer (_id, "Wrong");
        Debug.Log ("false2");
        GameEvent.instance.ChangeLife (-1);
    }

    void UpdateCustomerOrders (int[] orders, int _id) {
        seatsOrders[_id] = orders;
    }

    void FinalVIPCustomerHandle (int _id) {
        seatStats[_id] = false;
        GameEvent.instance.DespawnCustomer (_id, "Correct");
        GameEvent.instance.IncreaseScore (40);
        GameEvent.instance.ChangeMultiplier (1);
    }

    void WaitTimeoutHandler (int _id, string _type) {
        if (_type == "Normal") {
            GameEvent.instance.DecreaseScore (20);
        } else if (_type == "VIP") {
            GameEvent.instance.DecreaseScore (100);
        } else if (_type == "EatALot") {
            GameEvent.instance.DecreaseScore (40);
        }
        seatStats[_id] = false;
        GameEvent.instance.ChangeLife (-1);
        GameEvent.instance.DespawnCustomer (_id, "Timeout");
    }

    #endregion

    #region MonoBehaviour
    private void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }

    }

    private void OnEnable () {
        //GameEvent
        GameEvent.instance.OnCompare += CompareAnswersHandler;
        GameEvent.instance.OnReceiveNextVIPOrder += UpdateCustomerOrders;
        GameEvent.instance.OnFinalVIPOrder += FinalVIPCustomerHandle;
        GameEvent.instance.OnWaitTimeout += WaitTimeoutHandler;
    }
    private void OnDisable () {
        //GameEvent
        GameEvent.instance.OnCompare -= CompareAnswersHandler;
        GameEvent.instance.OnReceiveNextVIPOrder -= UpdateCustomerOrders;
        GameEvent.instance.OnFinalVIPOrder -= FinalVIPCustomerHandle;
        GameEvent.instance.OnWaitTimeout -= WaitTimeoutHandler;
    }
    private void OnDestroy () {
        GameEvent.instance.OnCompare -= CompareAnswersHandler;
        GameEvent.instance.OnReceiveNextVIPOrder -= UpdateCustomerOrders;
        GameEvent.instance.OnFinalVIPOrder -= FinalVIPCustomerHandle;
        GameEvent.instance.OnWaitTimeout -= WaitTimeoutHandler;
    }
    private void Start () {

        for (int i = 0; i < times.Length; i++) { SetRandomTime (i); }

        seatsOrders[0] = new int[3];
        seatsOrders[1] = new int[3];
        seatsOrders[2] = new int[3];
        seatsOrders[3] = new int[3];

    }

    private void Update () {
        for (int i = 0; i < times.Length; i++) {
            if (seatStats[i] == false) {
                times[i] += Time.deltaTime;
                if (times[i] >= spawnTimes[i]) {
                    SpawnObject (i);
                    SetRandomTime (i);
                }
            }

        }

    }
    #endregion

}