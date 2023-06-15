using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStat : MonoBehaviour
{
    public string CurTime;
    public float PlayTime;
    public string CurPos;
    public int GameTurn;
    public bool isNPC;
    public bool IsMapChoiced;
    public NPCMove NPCPosition;

    private void Awake()
    {
        GameTurn = 1;
        CurTime = "0";
        CurPos = "매표소";
        isNPC = false;
        NPCPosition = new NPCMove();
        PlayTime = 0;
        IsMapChoiced = false;
    }

    private void Update()
    {
        PlayTime += Time.deltaTime;
    }

    public void TurnIncrease()
    {
        //서버 시간 반영
        GameTurn++;
    }

    //NPC 위치 확인 후 해당 NPC 이름 반환
    public string CheckNPC(int Turn)
    {
        Debug.Log(Turn);
        if (CurPos == NPCPosition.RangerPos[Turn])
        {
            isNPC = true;  return "0";
        }
            
        else if (CurPos == NPCPosition.DogPos[Turn])
        {
            isNPC = true;  return "1";
        }
        else if (CurPos == NPCPosition.DollPos[Turn])
        {
            isNPC = true; return "2";
        }
        else
        {
            isNPC = false;  return "x";
        }
    }

    public string GetPlayTime()
    {
        int Min = Convert.ToInt32(PlayTime / 60);
        int Sec = Convert.ToInt32(PlayTime % 60);

        return Min + "분 " + Sec + "초";
    }
}
