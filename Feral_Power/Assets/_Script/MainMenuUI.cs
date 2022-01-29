using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    GameObject HowToPlayPanel;
    [SerializeField]
    GameObject CreditPanel;
    [SerializeField]
    GameObject MainPanel;

    public void Awake()
    {
        
    }

    public void Start()
    {
       MainPanel.SetActive(true);
        HowToPlayPanel.SetActive(false);
        CreditPanel.SetActive(false);
    }
    public void StartButton()
    {
        GameManager.Instance.ChangeScene("GameScene");
    }
    public void MainPanelOpen()
    {
        MainPanel.SetActive(true);
    }
    //public void OptionWindowOpen()
    //{
    //    optionPanel.SetActive(true);
    //}
    //public void OptionWindowClose()
    //{
    //    optionPanel.SetActive(false);
    //}
    public void HowToWindowOpen()
    {
        HowToPlayPanel.SetActive(true);
        MainPanel.SetActive(false);
    }
    public void HowToWindowClose()
    {
        HowToPlayPanel.SetActive(false);
        MainPanel.SetActive(true);
    }
    public void CreditWindowOpen()
    {
        CreditPanel.SetActive(true);
        MainPanel.SetActive(false);
    }
    public void CreditWindowClose()
    {
        CreditPanel.SetActive(false);
        MainPanel.SetActive(true);
    }
    public void LoadButton()
    {
        //SaveManager.Instance.Load();
        GameManager.Instance.ChangeScene("Loading");
    }
 
    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Clicked");
    }
}