using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCMove
{
    public List<string> DollPos { get; set; }
    public List<string> RangerPos { get; set; }
    public List<string> DogPos { get; set; }

    public NPCMove()
    {
        DogPos = new List<string>() { "광장", "매점", "회전목마", "매점", "광장"}; // 강아지 위치 설정
        SetMoving(); // 나머지 위치 설정
    }

    public void SetMoving()
    {
        DollPos = new List<string>() { "광장" };
        RangerPos = new List<string>() { "광장" };

        int a = UnityEngine.Random.Range(0, 2);

        if (a == 0)
            Setting1();
        else
            Setting2();
    }

    public void Setting1()
    {
        DollPos.Add("매점"); //인형탈 알바 위치 설정
        DollPos.Add("관람차");
        DollPos.Add("매점");
        DollPos.Add("광장");

        RangerPos.Add("익스트림 어트렉션"); // 관리인 위치 설정
        int b = UnityEngine.Random.Range(0, 2);

        if (b == 1) // 바이킹 or 롤러코스터
            RangerPos.Add("바이킹");
        else
            RangerPos.Add("롤러코스터");

        RangerPos.Add("익스트림 어트렉션");
        RangerPos.Add("광장");
    }

    public void Setting2()
    {
        RangerPos.Add("매점"); // 관리인 위치 설정
        RangerPos.Add("관람차");
        RangerPos.Add("매점");
        RangerPos.Add("광장");

        DollPos.Add("익스트림 어트렉션"); // 인형탈 알바 위치 설정
        int b = UnityEngine.Random.Range(0, 2);

        if (b == 1) // 바이킹 or 롤러코스터
            DollPos.Add("바이킹");
        else
            DollPos.Add("롤러코스터");

        DollPos.Add("익스트림 어트렉션");
        DollPos.Add("광장");
    }
}
