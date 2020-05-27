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
    void Start () {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update () {
        currentTime += Time.deltaTime;

    }

    private void TrackTime () {
        if (currentTime == stage1) {
            ChangeDifficulty (1);
        } else if (currentTime == stage2) {
            ChangeDifficulty (2);
        } else if (currentTime == stage3) {
            ChangeDifficulty (3);
        } else if (currentTime == stage4) {
            ChangeDifficulty (4);
        }
    }

    private void ChangeDifficulty (int stage) {

    }

    private void IncreaseDifficulty () {
        //use invoke repeating
    }
}