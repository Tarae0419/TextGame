using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyData : MonoBehaviour
{
    public CharacterData Characterdata;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetCharacter(int Select)
    {
        if (Select == 1)
        {
            Characterdata.Name = "아이 엄마";
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
        else if (Select == 2)
        {
            Characterdata.Name = "관리인";
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
        else if (Select == 3)
        {
            Characterdata.Name = "인형 알바";
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
        else if (Select == 4)
        {
            Characterdata.Name = "강아지";
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
    }
}
