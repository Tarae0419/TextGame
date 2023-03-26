using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject ExitPanel;

    void Start()
    {
        
    }

    void Update()
    {
        // close exitpanel
        if (OptionPanel.activeSelf == true && ExitPanel.activeSelf == true)
        {
            ExitPanel.SetActive(false);
        }
    }

    /*IEnumerator UnityWebRequestGet()
    {
        string  url = "";

        UnityWebRequestGet www = UnityWebRequestGet.Get(url);

        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log("ERROR");
        }
        else
        {

        }
    }*/

    public void Exit()
    {
        Application.Quit();
    }

    double TimerRatio(double ServerTime)
    {
        return 100 - ServerTime/30 * 100;
    }
}
