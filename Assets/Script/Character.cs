using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour
{
    public Image CurrentLocation;
    public CharacterData Characterdata;

    void Awake()
    {
        GameStart();
    }
    void Update()
    {
        PlayerLocation(Characterdata.CurrentMapName);
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
            Characterdata = Data.Characterdata;
        }
        Destroy(LobbyData);
    }
}
[System.Serializable]
public class CharacterData
{
    public string Name;
    public int HP;
    public int SAN;
    public int STR;
    public int END;
    public int CON;
    public int DEX;
    public int INT;
    public int EDU;
    public int INS;
    public int CHA;
    public int PROB;
    public string CurrentMapName; 
}

