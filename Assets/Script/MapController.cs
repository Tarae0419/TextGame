using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MapController : MonoBehaviour
{
    public Character Character;
    public TextMeshProUGUI FirstMap;
    public TextMeshProUGUI SecondMap;
    public TextMeshProUGUI ThirdMap;
    public string[] MapName;
    public GameObject MapButton;
    public Image TimerBar;


    private void Awake()
    {
        MapName = new string[6] {"매표소", "기념품점", "대로", "광장", "매점", "회전목마"};
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
            Character.CurrentMapIndex++;
        }
        else if (NextMap == 2) //back
        {
            Character.CurrentMapIndex--;
        }
        else //third map(현재 익스트림에트렉션일때 가능)
        {
            
        }
    }

    public void SetButton()
    {
        if(Character.CurrentMapIndex == 1) // 맨끝(관람차, 회전목마 등)일 시 버튼 하나만 or
        {
            //버튼 하나만 
        }
        
        FirstMap.text = MapName[Character.CurrentMapIndex];
        SecondMap.text = MapName[Character.CurrentMapIndex - 2];
        MapButton.SetActive(true);

    }
}
