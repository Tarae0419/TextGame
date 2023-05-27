using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatTextUI : MonoBehaviour
{
    public CharacterData Player;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI CurrentHPText;
    public TextMeshProUGUI CurrentSANText; 
    public TextMeshProUGUI CurrentSTRText;
    public TextMeshProUGUI CurrentENDText;
    public TextMeshProUGUI CurrentCONText;
    public TextMeshProUGUI CurrentDEXText;
    public TextMeshProUGUI CurrentINTText;
    public TextMeshProUGUI CurrentEDUText;
    public TextMeshProUGUI CurrentINSText;
    public TextMeshProUGUI CurrentCHAText;
    public TextMeshProUGUI CurrentPROBText;

    void Update()
    {
        SetCharacterStat();
    }

    public void SetCharacterStat() //current Stat
    {
        NameText.text = Player.Name;
        CurrentHPText.text = Player.HP.ToString();
        CurrentSANText.text = Player.SAN.ToString();
        CurrentSTRText.text = Player.STR.ToString();
        CurrentENDText.text = Player.END.ToString();
        CurrentCONText.text = Player.CON.ToString();
        CurrentDEXText.text = Player.DEX.ToString();
        CurrentINTText.text = Player.INT.ToString();
        CurrentEDUText.text = Player.EDU.ToString();
        CurrentINSText.text = Player.INS.ToString();
        CurrentCHAText.text = Player.CHA.ToString();
        CurrentPROBText.text = Player.PROB.ToString();
    }
}
