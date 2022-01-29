using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager instance = null;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion
    public void ChangeScene(string sceneName)
    {
        ResetAllVar();
        SceneManager.LoadScene(sceneName);
    }

    private static bool Day = true;

    public static GameObject Player = null;
    private static void ResetAllVar()
    {
        Day = true;
        Player = null;
    }

    public static void TimeChange()
    {
        Day = !Day;
    }

    public static bool IsNight()
    {
        return !Day;
    }

    public static bool IsDay()
    {
        return Day;
    }
}
