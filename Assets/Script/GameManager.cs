using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string CurTime;
    public string CurPos;
    public int GameTurn;

    private void Awake()
    {
        GameTurn = 1;
    }

    public void TurnIncrease()
    {
        //서버 시간 반영
        GameTurn++;
    }
}
