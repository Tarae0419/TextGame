using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    public GameObject MultiPanel;

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

    public void SetPanel(int Select)
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
}
