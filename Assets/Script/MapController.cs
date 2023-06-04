using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MapController : MonoBehaviour
{
    public GameObject MapButton;
    public GameObject MapPanel;
    public GameObject MapCountManager;
    public GameObject Button1;
    public GameObject Button2;
    public CharacterData PlayerData;
    public GameStat GameStat;
    public MapCount Mapcount;
    public string[] MorningMapName;
    public string[] LunchMapName;



    private void Awake()
    {
        MorningMapName = new string[14] {"매표소", "기념품점", "기념품점", "대로", "광장", "광장", "바이킹", "바이킹", "롤러코스터", "롤러코스터", "매점", "매점", "회전목마", "관람차" };
        LunchMapName = new string[4] { "대로", "기념품점", "매표소", "광장" };
        Mapcount = MapCountManager.GetComponent<MapCount>();
        SetMap("매표소,기념품점");
    }


    public void MapUpdate(string MapName) //맵 이동 버튼 클릭시 현재 맵 변경
    {
        GameStat.CurPos = MapName;
        PlayerData.CurrentMapName = MapName;
        MapPanel.SetActive(false);
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
                    /*
                     case "대로": Button2.SetActive(true); break;
                     case "광장": Button2.SetActive(true); break;
                     case "매점": Button2.SetActive(true); break;
                     case "회전목마": Button2.SetActive(true); break;
                     case "관람차": Button2.SetActive(true); break;
                     case "익스트림 어트렉션": Button2.SetActive(true); break;
                     case "롤러코스터": Button2.SetActive(true); break;
                     case "바이킹": Button2.SetActive(true); break;
                     case "거울의 미로": Button2.SetActive(true); break;
                     */
            }


        }
        MapPanel.SetActive(true);
    }
}
