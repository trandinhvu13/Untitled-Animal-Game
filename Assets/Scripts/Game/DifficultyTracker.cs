using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyTracker : MonoBehaviour {
    private float currentTime;
    public float stage1;
    public float stage2;
    public float stage3;
    public float stage4;
    public float stage4Time = 0;
    public int maxRepeatTime;
    private int currentRepeatTime = 0;
    private bool isChangedDifficulty = false;
    private void OnEnable() {
        currentTime = 0;
    }
    void Start () {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update () {
        currentTime += Time.deltaTime;
        TrackTime ();
    }

    private void TrackTime () {
        if (currentTime >= stage1 && currentTime < stage2 && !isChangedDifficulty) {
            isChangedDifficulty = true;
            ChangeDifficulty (1);
        } else if (currentTime >= stage2 && currentTime < stage3 && isChangedDifficulty) {
            isChangedDifficulty = false;
            ChangeDifficulty (2);
        } else if (currentTime >= stage3 && currentTime < stage4 && !isChangedDifficulty) {
            isChangedDifficulty = true;
            ChangeDifficulty (3);
        } else if (currentTime >= stage4 && isChangedDifficulty) {
            isChangedDifficulty = false;
            ChangeDifficulty (4);
        }
    }

    private void ChangeDifficulty (int stage) {
        Debug.Log ("Change state: " + stage);
        if (stage == 1) {
            PlayerStats.instance.maxCustomerCount = 1;
            PlayerStats.instance.minSpawnTime = 2;
            PlayerStats.instance.maxSpawnTime = 5;
            PlayerStats.instance.waitTime = 25;
        } else if (stage == 2) {
            PlayerStats.instance.maxCustomerCount = 2;
            PlayerStats.instance.minSpawnTime = 1;
            PlayerStats.instance.maxSpawnTime = 6;
            PlayerStats.instance.waitTime = 20;
        } else if (stage == 3) {
            PlayerStats.instance.maxCustomerCount = 3;
            PlayerStats.instance.minSpawnTime = 4;
            PlayerStats.instance.maxSpawnTime = 8;
            PlayerStats.instance.waitTime = 18;
        } else if (stage == 4 && currentRepeatTime < maxRepeatTime) {
            PlayerStats.instance.maxCustomerCount = 4;
            InvokeRepeating ("IncreaseDifficulty", stage4Time, stage4Time);
        }
    }

    private void IncreaseDifficulty () {
        currentRepeatTime++;
        Debug.Log ("Current time repeat " + currentRepeatTime);
        PlayerStats.instance.maxSpawnTime -= 1;
        PlayerStats.instance.waitTime -= 2;
    }
}