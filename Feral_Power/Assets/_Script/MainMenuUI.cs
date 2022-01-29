using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    GameObject optionPanel;
    [SerializeField]
    GameObject HowToPlayPanel;
    [SerializeField]
    GameObject CreditPanel;
    [SerializeField]
    GameObject MainPanel;

    public void Awake()
    {
        MainPanel.SetActive(true);
    }
    public void StartButton()
    {
        GameManager.Instance.ChangeScene("GameScene");
    }
    public void MainPanelOpen()
    {
        MainPanel.SetActive(true);
    }
    public void OptionWindowOpen()
    {
        optionPanel.SetActive(true);
    }
    public void OptionWindowClose()
    {
        optionPanel.SetActive(false);
    }
    public void HowToWindowOpen()
    {
        HowToPlayPanel.SetActive(true);
    }
    public void HowToWindowClose()
    {
        HowToPlayPanel.SetActive(false);
    }
    public void CreditWindowOpen()
    {
        CreditPanel.SetActive(true);
    }
    public void CreditWindowClose()
    {
        CreditPanel.SetActive(false);
    }
    //public void LoadButton()
    //{
    //    //SaveManager.Instance.Load();
    //    GameManager.Instance.ChangeScene("Loading");
    //}

    public void QuitButton()
    {
        Application.Quit();
    }
}