using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.U2D.Animation;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update


    public TextMeshProUGUI NameText;
    public TextMeshProUGUI HPText;
    public Image CurrentLocation;
    public CharacterData Characterdata;
    public int PlayerId;
    
    

    void Awake()
    {
        Characterdata.Name = "°­¾ÆÁö";
        Characterdata.CurrentMapIndex = 3;
        Characterdata.HP = 100;
        Characterdata.SAN = 80;
        Characterdata.STR = 15;
        Characterdata.END = 3;
        Characterdata.CON = 53;
        Characterdata.DEX = 12;
        Characterdata.INT = 24;
        Characterdata.EDU = 21;
        Characterdata.INS = 7;
        Characterdata.CHA = 10;
        Characterdata.PROB = 30;
}
    void Start()
    {
        
    }

    void Update()
    {
        SetCharacterStat();
        PlayerLocation(Characterdata.CurrentMapIndex);
    }

    public void SetCharacterStat()
    {
        NameText.text = Characterdata.Name;
        HPText.text = Characterdata.HP.ToString();
        //other stat
    }

    // change player location in map
    public void PlayerLocation(int CurrentMapIndex)
    {
        switch (CurrentMapIndex)
        {
            case 1: CurrentLocation.rectTransform.anchoredPosition = new Vector2(125, -50); break;
            case 2: CurrentLocation.rectTransform.anchoredPosition = new Vector2(0, -50); break;
            case 3: CurrentLocation.rectTransform.anchoredPosition = new Vector2(0, 75); break;
            case 4: CurrentLocation.rectTransform.anchoredPosition = new Vector2(0, 200); break;
            case 5: CurrentLocation.rectTransform.anchoredPosition = new Vector2(-125, 200); break;
            case 6: CurrentLocation.rectTransform.anchoredPosition = new Vector2(0, 325); break;
            default: break;
        }
    }

    public void GameStart(int id)
    {
        PlayerId = id;
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
    public int CurrentMapIndex;




}

