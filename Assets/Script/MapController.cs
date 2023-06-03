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
    public CharacterData PlayerData;
    public TextMeshProUGUI FirstMap;
    public TextMeshProUGUI SecondMap;
    public TextMeshProUGUI ThirdMap;
    public TextMeshProUGUI FourthMap; 
    public GameStat GameStat;
    public MapCount Mapcount;
    public Image TimerBar;
    public string[] MorningMapName;
    public string[] LunchMapName;



    private void Awake()
    {
        MorningMapName = new string[14] {"매표소", "기념품점", "기념품점", "대로", "광장", "광장", "바이킹", "바이킹", "롤러코스터", "롤러코스터", "매점", "매점", "회전목마", "관람차" };
        LunchMapName = new string[4] { "대로", "기념품점", "매표소", "광장" };
        Mapcount = MapCountManager.GetComponent<MapCount>();
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
            /*
             swtich(a)
            {
            case "맵이름": 버튼.SetActive(true); break;
            ...
            }
             
            */
        }
        MapPanel.SetActive(true);
    }
}
