using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update

    public int CurrentMapIndex;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI HPText;
    public Image CurrentLocation;
    public int HP;
    private string Name;
    private int SAN;
    private int STR;
    private int END;
    private int CON;
    private int DEX;
    private int INT;
    private int EDU;
    private int INS;
    private int CHA;
    private int PROB;
    

    void Awake()
    {
        Name = "°­¾ÆÁö";
        CurrentMapIndex = 3;
        HP = 100;
        SAN = 80;
        STR = 15;
        END = 3;
        CON = 53;
        DEX = 12;
        INT = 24;
        EDU = 21;
        INS = 7;
        CHA = 10;
        PROB = 30;
}
    void Start()
    {
        
    }

    void Update()
    {
        SetCharacterStat();
        PlayerLocation(CurrentMapIndex);
    }

    public void SetCharacterStat()
    {
        NameText.text = Name;
        HPText.text = HP.ToString();
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

}
