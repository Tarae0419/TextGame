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

    void Update()
    {
        SetCharacterStat();
    }

    public void SetCharacterStat()
    {
        NameText.text = Player.Characterdata.Name;
        CurrentHPText.text = Player.Characterdata.HP.ToString();
        //other stat
    }
}
