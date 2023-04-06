using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MapController : MonoBehaviour
{
    public CharacterData CharacterData;
    public TextMeshProUGUI FirstMap;
    public TextMeshProUGUI SecondMap;
    public TextMeshProUGUI ThirdMap;
    public string[] MapName;
    public GameObject MapButton;
    public Image TimerBar;


    private void Awake()
    {
        MapName = new string[11] {"매표소", "기념품점", "대로", "광장", "매점", "회전목마", "관람차", "익스트림 어트렉션", "롤러코스터", "귀신의 집", "바이킹"};
    }

    void Start()
    {
        
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
        yield return new WaitForSeconds(5f);
        if (NextMap == 1) //front
        {
            CharacterData.CurrentMapIndex++;
        }
        else if (NextMap == 2) //back
        {
            CharacterData.CurrentMapIndex--;
        }
        else if(NextMap ==3 )
        {
            //third map(현재 위치가 광장, 매점일 때)
        }
        else
        {
            //Fourth map(현재 위치가 익스트림 어트렉션일 때)
        }
    }

    public void SetButton()
    {
        if(CharacterData.CurrentMapIndex == 1) // 맨끝(관람차, 회전목마 등)일 시 버튼 하나만 or
        {
            //버튼 하나만 or x표시
            FirstMap.text = MapName[CharacterData.CurrentMapIndex];
            SecondMap.text = "X";
            ThirdMap.text = "X";
        }
        else if(CharacterData.CurrentMapIndex == 3)
        {
            FirstMap.text = MapName[CharacterData.CurrentMapIndex + 1];
            SecondMap.text = MapName[CharacterData.CurrentMapIndex + 4];
            ThirdMap.text = MapName[CharacterData.CurrentMapIndex - 1];
        }
        else if(CharacterData.CurrentMapIndex == 4)
        {
            FirstMap.text = MapName[CharacterData.CurrentMapIndex];
            SecondMap.text = MapName[CharacterData.CurrentMapIndex + 3];
            ThirdMap.text = MapName[CharacterData.CurrentMapIndex - 2];
        }
        else if(CharacterData.CurrentMapIndex == 5 )
        {
            FirstMap.text = MapName[CharacterData.CurrentMapIndex];
            SecondMap.text = MapName[CharacterData.CurrentMapIndex + 1];
            ThirdMap.text = MapName[CharacterData.CurrentMapIndex - 2];
        }
        else if(CharacterData.CurrentMapIndex == 6)
        {
            FirstMap.text = MapName[CharacterData.CurrentMapIndex - 3];
            SecondMap.text = "X";
            ThirdMap.text = "X";
        }
        else if(CharacterData.CurrentMapIndex == 8)
        {
            FirstMap.text = MapName[CharacterData.CurrentMapIndex];
            SecondMap.text = MapName[CharacterData.CurrentMapIndex + 1];
            ThirdMap.text = MapName[CharacterData.CurrentMapIndex + 2];
        }
        else
        {
            FirstMap.text = MapName[CharacterData.CurrentMapIndex];
            SecondMap.text = MapName[CharacterData.CurrentMapIndex - 2];
            ThirdMap.text = "X";
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
}
