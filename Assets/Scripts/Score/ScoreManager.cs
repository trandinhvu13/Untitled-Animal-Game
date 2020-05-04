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

    #region UI
    public GameObject score;
    public GameObject multiplier;
    #endregion

    #region Variables
    public int currentScore = 0;
    public int currentMultiplier = 1;
    public int correctOrderStreak = 0;
    public bool isOnMultiplier;

    public int currentLife;
    //public int scoreTo1Up;
    #endregion

    #region Monos
    private void OnEnable () {
        currentLife = PlayerStats.instance.maxLife;
        currentScore = 0;
        currentMultiplier = 1;
        GameEvent.instance.OnIncreaseScore += IncreaseScore;
        GameEvent.instance.OnDecreaseScore += DecreaseScore;
        GameEvent.instance.OnChangeMultiplier += ChangeMultiplier;
        GameEvent.instance.OnChangeLife += ChangeCurrentLife;
        GameEvent.instance.OnChangeMaxLife += ChangeCurrentMaxLife;

    }

    private void OnDisable () {
        GameEvent.instance.OnIncreaseScore -= IncreaseScore;
        GameEvent.instance.OnDecreaseScore -= DecreaseScore;
        GameEvent.instance.OnChangeMultiplier -= ChangeMultiplier;
        GameEvent.instance.OnChangeLife -= ChangeCurrentLife;
        GameEvent.instance.OnChangeMaxLife -= ChangeCurrentMaxLife;
    }

    void Update () {
        UI ();
        MultiplierTrack ();
        LifeTrack ();
        StreakTrack ();
    }
    #endregion

    #region Methods
    void UI () {
        if (currentScore > 0) {
            currentScoreText.text = currentScore.ToString ();
        } else {
            currentScore = 0;
            currentScoreText.text = "0";
        }

        if (currentMultiplier <= 1) {
            multiplierText.text = null;
        } else {
            multiplierText.text = "x " + currentMultiplier.ToString ();
            multiplierText.color = HSBColor.ToColor (new HSBColor (Mathf.PingPong (Time.time * 1, 1), 1, 1));
        }

    }

    void MultiplierTrack () {
        if (currentMultiplier >= PlayerStats.instance.maxMultiplier) {
            currentMultiplier = PlayerStats.instance.maxMultiplier;
        }
    }

    void LifeTrack () {
        if (currentLife <= 0) {
            Debug.Log ("EndGame");
        }
        // if (currentScore % scoreTo1Up==0)
        // {
        //     ChangeCurrentLife(1);
        // }
    }
    void StreakTrack () {
        if (correctOrderStreak == PlayerStats.instance.ordersToIncreaseMult) {
            ChangeMultiplier (1);
            correctOrderStreak = 0;
        }

    }

    void AddToTotalScore (int amount) {
        PlayerStats.instance.totalScore += amount;
    }
    void IncreaseScore (int scoreAmount) {
        currentScore = currentScore + (scoreAmount * currentMultiplier);
        Debug.Log ("IncreaseScore");
        LeanTween.scale (score, new Vector3 (1.5f, 1.5f, 1.5f), 0.35f).setEase (LeanTweenType.easeOutBack).setLoopPingPong (1);
        correctOrderStreak++;
    }

    void DecreaseScore (int scoreAmount) {
        currentScore = currentScore - scoreAmount;
        LeanTween.scale (score, new Vector3 (1.5f, 1.5f, 1.5f), 0.35f).setEase (LeanTweenType.easeOutBack).setLoopPingPong (1);
        currentMultiplier = 1;
        correctOrderStreak = 0;
    }

    void ChangeMultiplier (int multiplierAmount) {
        currentMultiplier += multiplierAmount;
        LeanTween.scale (multiplier, new Vector3 (1.5f, 1.5f, 1.5f), 0.35f).setEase (LeanTweenType.easeOutBack).setLoopPingPong (1);
        //ui effects

    }

    void ChangeCurrentLife (int amount) {
        currentLife += amount;
    }

    void ChangeCurrentMaxLife (int amount) {
        PlayerStats.instance.maxLife += amount;
    }
    #endregion

}