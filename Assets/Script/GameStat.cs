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
    public NPCMove NPCPosition;

    private void Awake()
    {
        GameTurn = 1;
        CurTime = "0";
        CurPos = "매표소";
        isNPC = false;
        NPCPosition = new NPCMove();
        PlayTime = 0;
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
        if (CurPos == NPCPosition.DollPos[Turn])
        {
            isNPC = true;  return "인형탈 알바";
        }
            
        else if (CurPos == NPCPosition.RangerPos[Turn])
        {
            isNPC = true;  return "관리인";
        }
            
        else if (CurPos == NPCPosition.DogPos[Turn])
        {
            isNPC = true;  return "강아지";
        }
        else
        {
            isNPC = false;  return "";
        }
    }

    public string GetPlayTime()
    {
        int Min = Convert.ToInt32(PlayTime / 60);
        int Sec = Convert.ToInt32(PlayTime % 60);

        return Min + "분 " + Sec + "초";
    }
}
