using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserHUD : MonoBehaviour
{
    public GameObject sun;
    public GameObject moon;
    public Text objective;
    public static bool timeUp = false;
    private float startTime = 10.0f;
    private float currentTime;

    [Header("Score")]
    public PlayerScoreManager ScoreManager;
    public Text Score;
    public Text Combo;
    


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.IsDay())
        {
            sun.SetActive(true);
            moon.SetActive(false);
        }
        else if (GameManager.IsNight())
        {
            moon.SetActive(true);
            sun.SetActive(false);
        }

        if (currentTime >= 0.0f)
        {
            currentTime -= Time.fixedDeltaTime;
            objective.text = "Current Objective\n" + "Survive for " + (int)currentTime + " seconds";
        }
        else
        {
            timeUp = true;
            objective.text = "Current Objective\n" + "Defeat The BossToad";
        }

        

    }
}
