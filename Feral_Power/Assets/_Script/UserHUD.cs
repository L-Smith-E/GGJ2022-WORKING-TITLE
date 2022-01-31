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
    private float startTime = 240.0f;
    private float currentTime;


    [Header("Score")]
    public PlayerScoreManager ScoreManager;
    public Text Score;
    public Text Combo;

    private bool battleStart = false;

    [Header("Player Info")]
    public ProjectileManager PlayerProjectileManager;
    public Text Health;
    public Text ProjectileCount;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TriggerGauntlet.startGauntlet == true)
        {
            battleStart = TriggerGauntlet.startGauntlet;
        }

        sun.SetActive(GameManager.IsDay());
        moon.SetActive(GameManager.IsNight());


        if (currentTime >= 0.0f && battleStart == true)
        {
            currentTime -= Time.fixedDeltaTime;
            objective.text = "Current Objective\n" + "Survive for " + (int)currentTime + " seconds";
        }
        else if (currentTime <= 0.0f)
        {
            timeUp = true;
            objective.text = "Current Objective\n" + "Defeat The BossToad";
        }
        else
        {
            objective.text = "Current Objective\n" + "Enter The Arena";
        }

        if(ScoreManager != null)
        {
            Score.text = "Score:" + ScoreManager.CurrentScore.ToString();

            Combo.text  = ScoreManager.CurrentMultiplier.ToString();
        }


        if (PlayerProjectileManager != null)
        {
            ProjectileCount.text = PlayerProjectileManager.ProjectileCount.ToString() + "/" + PlayerProjectileManager.InactiveProjectile;
        }

        if(GameManager.IsDay())
        {
            Score.color = Color.black;
            Combo.color = Color.black;
            Health.color = Color.black;
            ProjectileCount.color = Color.blue;
        }
        else
        {
            Score.color = Color.white;
            Combo.color = Color.white;
            Health.color = Color.white;
            ProjectileCount.color = Color.red;
        }
    }
}
