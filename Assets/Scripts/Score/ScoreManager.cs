using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    public int currentMultiplier = 1;
    public int maxMultiplier;
    public float multiplierDuration;
    public int correctOrderStreak = 0;
    public float currentMultiplierTime;
    public int multiplierOrderStep;
    public bool isOnMultiplier;

    public int maxLife;
    public int currentLife;
    public int scoreTo1Up;

    void Start()
    {
        GameEvent.instance.OnIncreaseScore += IncreaseScore;
        GameEvent.instance.OnIncreaseScore += DecreaseScore;
        GameEvent.instance.OnIncreaseScore += ChangeMultiplier;
        GameEvent.instance.OnChangeLife += ChangeCurrentLife;
        GameEvent.instance.OnChangeMaxLife += ChangeCurrentMaxLife;

    }

    private void OnDisable()
    {
        GameEvent.instance.OnIncreaseScore -= IncreaseScore;
        GameEvent.instance.OnIncreaseScore -= DecreaseScore;
        GameEvent.instance.OnIncreaseScore -= ChangeMultiplier;
        GameEvent.instance.OnChangeLife -= ChangeCurrentLife;
        GameEvent.instance.OnChangeMaxLife -= ChangeCurrentMaxLife;
    }

    void Update()
    {
        if (currentScore <= 0)
        {
            currentScore = 0;
        }

        if (currentMultiplierTime <= 0)
        {
            isOnMultiplier = false;
            currentMultiplier = 1;
        }
        else
        {
            isOnMultiplier = true;
            currentMultiplierTime -= Time.deltaTime;
        }

        if (currentScore % scoreTo1Up==0)
        {
            ChangeCurrentLife(1);
        }

        if (currentLife <= 0)
        {
            //Endgame
        }

        TrackStreak();
    }

    void TrackStreak()
    {
        if (isOnMultiplier)
        {
            if (correctOrderStreak >= multiplierOrderStep)
            {
                correctOrderStreak = 0;
                currentMultiplierTime = multiplierDuration;
                if (currentMultiplier <= maxMultiplier)
                {
                    currentMultiplier++;
                }
                else
                {
                    currentMultiplier = maxMultiplier;
                }
            }
        }
        else
        {
            if (correctOrderStreak == 1)
            {
                currentMultiplierTime = multiplierDuration;
            }
        }


    }
    void IncreaseScore(int scoreAmount)
    {
        currentScore += scoreAmount * currentMultiplier;
        correctOrderStreak++;

    }

    void DecreaseScore(int scoreAmount)
    {
        currentScore -= scoreAmount;
        currentMultiplier = 1;
        correctOrderStreak = 0;
    }

    void ChangeMultiplier(int multiplierAmount)
    {
        currentMultiplier += multiplierAmount;
    }

    void ChangeCurrentLife(int amount)
    {
        currentLife += amount;
    }

    void ChangeCurrentMaxLife(int amount)
    {
        maxLife += amount;
    }

}
