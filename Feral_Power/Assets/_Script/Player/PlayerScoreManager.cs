using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum ScoreMultiplier
{
    F = 0,
    E = 1,
    D = 2,
    C = 3,
    B = 4,
    A = 5,
    S = 6
}
public class PlayerScoreManager : MonoBehaviour
{

    [Header("Score")]
    [SerializeField]
    public Int64 CurrentScore;

    [SerializeField]
    private long ScoreIncreaseOnHit;

    [SerializeField]
    private bool LogScoreToConsole = false;
    [Header("Score Multiplier")]

    [SerializeField]
    public ScoreMultiplier CurrentMultiplier;

    [SerializeField]
    private int Combo;

    [SerializeField]
    private float HitBeforeIncrease;

    [SerializeField]
    private float TimeBeforeReset;

    [SerializeField]
    private float ResetTimer;


    // Start is called before the first frame update
    void Start()
    {
        CurrentMultiplier = 0;
    }

    private void FixedUpdate()
    {

        if (LogScoreToConsole)
            DisplayCurrentScorToConsole();

        //CurrentMultiplier = (ScoreMultiplier)1;

        ResetTimer += Time.fixedDeltaTime;
        if (ResetTimer >= TimeBeforeReset)
        {
            Combo = 0;
            CurrentMultiplier = 0;
            ResetTimer = 0;
        }

    }

    void DisplayCurrentScorToConsole()
    {
        Debug.Log("Multiplier: " + CurrentMultiplier);
        Debug.Log("Score: " + CurrentScore);
    }

    void HitEvent()
    {
        long Multiplier = (long)CurrentMultiplier;
        CurrentScore += ScoreIncreaseOnHit * Multiplier;
        ResetTimer = 0;
        Combo++;

        if(Mathf.Pow(HitBeforeIncrease,  Multiplier) <= Combo && CurrentMultiplier != ScoreMultiplier.S)
        {
            CurrentMultiplier = (ScoreMultiplier)(Multiplier + 1);
        }
    }
}
