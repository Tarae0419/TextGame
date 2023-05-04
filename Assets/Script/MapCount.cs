using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCount : MonoBehaviour
{
    public GameObject Player;
    public int[] MapInCount;

    private void Awake()
    {
        MapInCount = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void IncreaseMapCount(string MapName)
    {
        switch (MapName)
        {
            case "매표소": MapInCount[0]++; break;
            case "기념품점": MapInCount[1]++; break;
            case "대로": MapInCount[2]++; break;
            case "광장": MapInCount[3]++; break;
            case "매점": MapInCount[4]++; break;
            case "회전목마": MapInCount[5]++; break;
            case "관람차": MapInCount[6]++; break;
            case "익스트림 어트렉션": MapInCount[7]++; break;
            case "롤러코스터": MapInCount[8]++; break;
            case "귀신의집": MapInCount[9]++; break;
            case "바이킹": MapInCount[10]++; break;
        }
    }
}
