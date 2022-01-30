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
    private bool battleStart = false;
    


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

        

    }
}
