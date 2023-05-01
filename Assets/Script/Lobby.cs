using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    public GameObject LobbyPanel;
    public GameObject MultiPanel;
    public GameObject CharacterPanel;
    public GameObject ErrorPanel;
    private SelectCharacter SetChar;

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
        {
            MultiPanel.gameObject.SetActive(true);
        }
        else
        {
            MultiPanel.gameObject.SetActive(false);
        }
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
        {
            ErrorPanel.gameObject.SetActive(true);
        }
        else
        {
            ErrorPanel.gameObject.SetActive(false);
        }
    }

    public void SelectCharacter(int CharNum)
    {
        SetChar.SetCharacter(CharNum);
    }
}
