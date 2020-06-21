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
        if (stage == 1) {
            PlayerStats.instance.maxCustomerCount = 1;
            PlayerStats.instance.minSpawnTime = 3;
            PlayerStats.instance.maxSpawnTime = 14;
            PlayerStats.instance.waitTime = 40;
        } else if (stage == 2) {
            PlayerStats.instance.maxCustomerCount = 2;
            PlayerStats.instance.minSpawnTime = 5;
            PlayerStats.instance.maxSpawnTime = 17;
            PlayerStats.instance.waitTime = 35;
        } else if (stage == 3) {
            PlayerStats.instance.maxCustomerCount = 3;
            PlayerStats.instance.minSpawnTime = 10;
            PlayerStats.instance.maxSpawnTime = 22;
            PlayerStats.instance.waitTime = 35;
        } else if (stage == 4 && currentRepeatTime < maxRepeatTime) {
            PlayerStats.instance.maxCustomerCount = 4;
            InvokeRepeating ("IncreaseDifficulty", stage4Time, stage4Time);
        }
    }

    private void IncreaseDifficulty () {
        currentRepeatTime++;
        PlayerStats.instance.maxSpawnTime -= 1;
        PlayerStats.instance.waitTime -= 2;
    }
}