using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MapController : MonoBehaviour
{
    public GameObject MapPanel;
    public GameObject MapCountManager;
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;
    public GameObject Button5;
    public GameObject Button6;
    public GameObject Button7;
    public GameObject Button8;
    public GameObject Button9;
    public GameObject Button10;
    public GameObject Button11;
    public CharacterData PlayerData;
    public GameStat GameStat;
    public MapCount Mapcount;
    public int CurRow;
    public int CurColumn;
    public string[] MorningMapName;
    public string[] LunchMapName;
    public string[,] MapName;

    private void Awake()
    {
        MapName = new string[4, 6] { { "1","1","1","관람차","1","1"},
                                     { "1","롤러코스터","1","매점","회전목마", "1"},
                                     { "귀신의집", "익스트림 어트렉션","광장","광장","1","1"},
                                     { "1","바이킹","1","대로","기념품점","매표소"} };
        Mapcount = MapCountManager.GetComponent<MapCount>();
        GameStat = gameObject.GetComponent<GameStat>(); 
    }

    public void MapUpdate(string MapName) //맵 이동 버튼 클릭시 현재 맵 변경
    {
        GameStat.CurPos = MapName;
        PlayerData.CurrentMapName = MapName;
        switch (MapName)
        {
            case "매표소": CurRow = 3; CurColumn = 5; break;
            case "기념품점": CurRow = 3; CurColumn = 4; break;
            case "대로": CurRow = 3; CurColumn = 3; break;
            case "광장": CurRow = 2; CurColumn = 3; break;
            case "매점": CurRow = 1; CurColumn = 3; break;
            case "회전목마": CurRow = 1; CurColumn = 4; break;
            case "관람차": CurRow = 0; CurColumn = 3; break;
            case "익스트림 어트렉션": CurRow = 2; CurColumn = 1; break;
            case "롤러코스터": CurRow = 1; CurColumn = 1; break;
            case "바이킹": CurRow = 3; CurColumn = 1; break;
            case "거울의 미로": CurRow = 2; CurColumn = 0; break;
        }
        GameStat.CurPos = this.MapName[CurRow, CurColumn];
        GameStat.IsMapChoiced = true;
        MapPanel.SetActive(false);
        ButtonOff();
    }

    public void SetMap(string Map) //맵 UI 활성화
    {
        IEnumerable<string> MapData = Map.Split(',');

        foreach(var a in MapData)
        {    
             switch(a)
            {
                case "매표소": Button1.SetActive(true); break;
                case "기념품점": Button2.SetActive(true); break;
                case "대로": Button3.SetActive(true); break;
                case "광장": Button4.SetActive(true); break;
                case "매점": Button5.SetActive(true); break;
                case "회전목마": Button6.SetActive(true); break;
                case "관람차": Button7.SetActive(true); break;
                case "익스트림 어트렉션": Button8.SetActive(true); break;
                case "롤러코스터": Button9.SetActive(true); break;
                case "바이킹": Button10.SetActive(true); break;
                case "거울의 미로": Button11.SetActive(true); break;
                case "1": break;
                     
            }
        }
        MapPanel.SetActive(true);
    }

    public void SetButton() //MapPosition이 0일때 불러오는 함수
    {
        int[] xDir = { -1, 0, 1, 0 };
        int[] yDir = { 0, -1, 0, 1 };
        int Count = 0;

        for (int dir = 0; dir < 4; dir++)
        {
            int newX = CurRow + xDir[dir];
            int newY = CurColumn + yDir[dir];

            if ((newX >= 0 && newX < 4) && (newY >= 0 && newY < 6) && MapName[newX, newY] != "1")
            {
                switch (Count)
                {
                    case 0: SetMap(MapName[newX, newY]); break;
                    case 1: if (GameStat.CurPos == "광장") SetMap("익스트림 어트렉션"); else SetMap(MapName[newX, newY]); break;
                    case 2: SetMap(MapName[newX, newY]); break;
                    case 3: SetMap(MapName[newX, newY]); break;
                }
                Count++;
            }
        }
        if (CurRow == 2 && (CurColumn == 2 || CurColumn == 3))
        {
            CurColumn = 3;
        }

    }

    public void ButtonOff()
    {
        Button1.SetActive(false);
        Button2.SetActive(false);
        Button3.SetActive(false);
        Button4.SetActive(false);
        Button5.SetActive(false);
        Button6.SetActive(false);
        Button7.SetActive(false);
        Button8.SetActive(false);
        Button9.SetActive(false);
        Button10.SetActive(false);
        Button11.SetActive(false);
    }
}
