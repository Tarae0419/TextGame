using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    public GameObject LobbyPanel;
    public GameObject MultiPanel;
    public GameObject CharacterPanel;
    public GameObject ErrorPanel;
    public GameObject ExitPanel;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetMultiPanel(int Select)
    {
        if(Select == 1)
            MultiPanel.gameObject.SetActive(true);
        else
            MultiPanel.gameObject.SetActive(false);
    }
    public void SetCharcterPanel(int Select)
    {
        if (Select == 1)
        {
            CharacterPanel.gameObject.SetActive(true);
            LobbyPanel.gameObject.SetActive(false);
        }
        else
        {
            CharacterPanel.gameObject.SetActive(false);
            LobbyPanel.gameObject.SetActive(true);
        }
    }

    public void SetErrorPanel(int Select)
    {
        if (Select == 1)
            ErrorPanel.gameObject.SetActive(true);
        else
            ErrorPanel.gameObject.SetActive(false);
    }

    public void SetExitPanel(int Select)
    {
        if (Select == 1) 
            ExitPanel.gameObject.SetActive(true);
        else
            ExitPanel.gameObject.SetActive(false);
    }

    public void test(GameObject a, int sum)
    {
        a.gameObject.SetActive(false);
    }
}
