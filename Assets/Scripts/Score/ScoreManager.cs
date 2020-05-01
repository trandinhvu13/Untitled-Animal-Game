using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager instance = null;

    private void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    #region GameObj
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI multiplierText;
    #endregion

    #region Variables
    public int currentScore = 0;
    public int currentMultiplier = 1;
    public float multiplierDuration;
    public int correctOrderStreak = 0;
    public float currentMultiplierTime;
    public int multiplierOrderStep;
    public bool isOnMultiplier;

    public int currentLife;
    //public int scoreTo1Up;
    #endregion

    #region Monos
    private void OnEnable () {
        currentLife = PlayerStats.instance.maxLife;
        currentScore = 0;
        GameEvent.instance.OnIncreaseScore += IncreaseScore;
        GameEvent.instance.OnIncreaseScore += DecreaseScore;
        GameEvent.instance.OnIncreaseScore += ChangeMultiplier;
        GameEvent.instance.OnChangeLife += ChangeCurrentLife;
        GameEvent.instance.OnChangeMaxLife += ChangeCurrentMaxLife;

    }

    private void OnDisable () {
        GameEvent.instance.OnIncreaseScore -= IncreaseScore;
        GameEvent.instance.OnIncreaseScore -= DecreaseScore;
        GameEvent.instance.OnIncreaseScore -= ChangeMultiplier;
        GameEvent.instance.OnChangeLife -= ChangeCurrentLife;
        GameEvent.instance.OnChangeMaxLife -= ChangeCurrentMaxLife;
    }

    void Update () {
        UI ();
        ScoreTrack ();
        //MultiplierTrack ();
        LifeTrack ();
       // StreakTrack ();
    }
    #endregion

    #region Methods
    void UI () {
        currentScoreText.text = currentScore.ToString ();
        multiplierText.color = HSBColor.ToColor(new HSBColor( Mathf.PingPong(Time.time * 1, 1), 1, 1));
        if (currentMultiplier <= 1) {
            multiplierText.text = null;
        } else {
            multiplierText.text = "x " + currentMultiplier.ToString ();
        }

    }
    void ScoreTrack () {
       
    }

    void MultiplierTrack () {
        if (currentMultiplierTime <= 0) {
            isOnMultiplier = false;
            currentMultiplier = 1;
        } else {
            isOnMultiplier = true;
            currentMultiplierTime -= Time.deltaTime;
        }
    }

    void LifeTrack () {
        if (currentLife <= 0) {
            //Endgame
        }
        // if (currentScore % scoreTo1Up==0)
        // {
        //     ChangeCurrentLife(1);
        // }
    }
    void StreakTrack () {
        if (isOnMultiplier) {
            if (correctOrderStreak >= multiplierOrderStep) {
                correctOrderStreak = 0;
                currentMultiplierTime = multiplierDuration;
                if (currentMultiplier <= PlayerStats.instance.maxMultiplier) {
                    currentMultiplier++;
                } else {
                    currentMultiplier = PlayerStats.instance.maxMultiplier;
                }
            }
        } else {
            if (correctOrderStreak == 1) {
                currentMultiplierTime = multiplierDuration;
            }
        }

    }

    void AddToTotalScore (int amount) {
        PlayerStats.instance.totalScore += amount;
    }
    void IncreaseScore (int scoreAmount) {
        currentScore += scoreAmount * currentMultiplier;
        Debug.Log("IncreaseScore");
        correctOrderStreak++;

    }

    void DecreaseScore (int scoreAmount) {
        currentScore -= scoreAmount;
        currentMultiplier = 1;
        correctOrderStreak = 0;
    }

    void ChangeMultiplier (int multiplierAmount) {
        currentMultiplier += multiplierAmount;
        
    }

    void ChangeCurrentLife (int amount) {
        currentLife += amount;
    }

    void ChangeCurrentMaxLife (int amount) {
        PlayerStats.instance.maxLife += amount;
    }
    #endregion

}