using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class StatTextManager : MonoBehaviour
{
    public Character Player;
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
        NameText.text = Player.Characterdata.Name;
        CurrentHPText.text = Player.Characterdata.HP.ToString();
        CurrentSANText.text = Player.Characterdata.SAN.ToString();
        CurrentSTRText.text = Player.Characterdata.STR.ToString();
        CurrentENDText.text = Player.Characterdata.END.ToString();
        CurrentCONText.text = Player.Characterdata.CON.ToString();
        CurrentDEXText.text = Player.Characterdata.DEX.ToString();
        CurrentINTText.text = Player.Characterdata.INT.ToString();
        CurrentEDUText.text = Player.Characterdata.EDU.ToString();
        CurrentINSText.text = Player.Characterdata.INS.ToString();
        CurrentCHAText.text = Player.Characterdata.CHA.ToString();
        CurrentPROBText.text = Player.Characterdata.PROB.ToString();
    }
}
