using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class PuppyData : LobbyData
{
    public override void InitSetting()
    {
        Characterdata.Name = "강아지";
        Characterdata.HP = 100;
        Characterdata.SAN = 80;
        Characterdata.STR = 15;
        Characterdata.END = 3;
        Characterdata.CON = 53;
        Characterdata.DEX = 1;
        Characterdata.INT = 24;
        Characterdata.EDU = 21;
        Characterdata.INS = 7;
        Characterdata.CHA = 10;
        Characterdata.PROB = 30;
        Characterdata.CurrentMapName = "매표소";
    }
}
