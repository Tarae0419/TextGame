using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MapController : MonoBehaviour
{
    public GameObject MapButton;
    public GameObject MapCountManager;
    public GameObject Player;
    public GameManager gameManager;
    private CharacterData PlayerData;
    public TextMeshProUGUI FirstMap;
    public TextMeshProUGUI SecondMap;
    public TextMeshProUGUI ThirdMap;
    public TextMeshProUGUI FourthMap;
    public MapCount Mapcount;
    public Image TimerBar;
    public string[,] MapName;
    public int CurRow;
    public int CurColumn;
    private int nextX1;
    private int nextY1;
    private int nextX2;
    private int nextY2;
    private int nextX3;
    private int nextY3;
    private int nextX4;
    private int nextY4;

    private void Awake()
    {
        MapName = new string[4, 6] { { "1","1","1","관람차","1","1"},
                                     { "1","롤러코스터","1","매점","회전목마", "1"},
                                     { "거울의 미로", "익스트림 어트렉션","광장","광장","1","1"},
                                     { "1","바이킹","1","대로","기념품점","매표소"} };
        CurRow = 3;
        CurColumn = 5;
        Mapcount = MapCountManager.GetComponent<MapCount>();
        PlayerData = Player.GetComponent<Character>().Characterdata;
        gameManager = GetComponent<GameManager>();
    }

    void Update()
    {
        if(TimerBar.fillAmount == 0)
        {
            SetButton();
        }
    }

    //button click event
    public void SelectMap(int NextMap)
    {
        StartCoroutine(ChangeMap(NextMap));
    }

    IEnumerator ChangeMap(int NextMap)
    {
        yield return new WaitForSeconds(3f);
        if (NextMap == 1) 
        {
            CurRow = nextX1;
            CurColumn = nextY1;
        }
        else if (NextMap == 2) 
        {
            CurRow = nextX2;
            CurColumn = nextY2;
        }
        else if(NextMap ==3 )
        {       
            CurRow = nextX3;
            CurColumn = nextY3;
        }
        else
        {
            CurRow = nextX4;
            CurColumn = nextY4;
        }
        
        MapButton.SetActive(false);
        TimerBar.fillAmount = 1;
        PlayerData.CurrentMapName = MapName[CurRow, CurColumn];
        Mapcount.IncreaseMapCount(PlayerData.CurrentMapName); //increase map incount
        gameManager.TurnIncrease();
        ResetButtonText();
        // start next?
    }

    public void SetButton()
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
                    case 0: FirstMap.text = MapName[newX, newY]; nextX1 = newX; nextY1 = newY; break;
                    case 1: SecondMap.text = MapName[newX, newY]; nextX2 = newX; nextY2 = newY; break;
                    case 2: ThirdMap.text = MapName[newX, newY]; nextX3 = newX; nextY3 = newY; break;
                    case 3: FourthMap.text = MapName[newX, newY]; nextX4 = newX; nextY4 = newY; break;
                }
                Count++;
            }
        }
        if (CurRow == 2 && (CurColumn == 2 || CurColumn == 3))
        {
            CurColumn = 3;
            SecondMap.text = MapName[nextX2, nextY2 - 1];
            nextY2 -= 1;
        }
        SetButtonPosition();
        MapButton.SetActive(true);

    }

    public void SetButtonPosition()
    {
        //one button

        //two buttons

        //three buttons

        //four buttons
    }

    public void ResetButtonText()
    {
        FirstMap.text = "";
        SecondMap.text = "";
        ThirdMap.text = "";
        FourthMap.text = "";
    }

}
