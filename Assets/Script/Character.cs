using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour
{
    public Image CurrentLocation;
    public CharacterData Chardata;

    void Awake()
    {
        Chardata = gameObject.GetComponent<CharacterData>();
        GameStart();
    }
    void Update()
    {
        PlayerLocation(Chardata.CurrentMapName);
    }

    // change player location in map
    public void PlayerLocation(string CurrentMapName)
    {
        /*switch (CurrentMapName)
        {
            case 1: CurrentLocation.rectTransform.anchoredPosition = new Vector2(125, -50); break;
            case 2: CurrentLocation.rectTransform.anchoredPosition = new Vector2(0, -50); break;
            case 3: CurrentLocation.rectTransform.anchoredPosition = new Vector2(0, 75); break;
            case 4: CurrentLocation.rectTransform.anchoredPosition = new Vector2(0, 200); break;
            case 5: CurrentLocation.rectTransform.anchoredPosition = new Vector2(-125, 200); break;
            case 6: CurrentLocation.rectTransform.anchoredPosition = new Vector2(0, 325); break;
            default: break;
        }*/
    }

    public void GameStart()
    {
        //inject data
        GameObject LobbyData = GameObject.Find("LobbyData");
        if (LobbyData == null)
        {
            Debug.Log("ERROR");
        }
        else
        {
            LobbyData Data = LobbyData.GetComponent<LobbyData>();
            Chardata = Data.Characterdata;
        }
        Destroy(LobbyData);
    }

    public void AddCharStat(string stat, int num) // 스탯 증가 함수
    {
        switch (stat)
        { 
            case "HP": Chardata.HP += num; break;
            case "SAN": Chardata.SAN += num; break;
            case "STR": Chardata.STR += num; break;
            case "END": Chardata.END += num; break;
            case "CON": Chardata.CON += num; break;
            case "DEX": Chardata.DEX += num; break;
            case "INT": Chardata.INT += num; break;
            case "EDU": Chardata.EDU += num; break;
            case "INS": Chardata.INS += num; break;
            case "CHA": Chardata.CHA += num; break;
            case "PROB": Chardata.PROB += num; break;
        }

    }

}