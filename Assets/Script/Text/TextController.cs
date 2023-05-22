using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    private string[] TextData;
    public TextMeshProUGUI StoryText;
    public GameObject FirstButton;
    public GameObject SecondButton;
    public GameObject ThirdButton;
    public GameObject FourthButton;
    public GameManager GameManager;
    public DataManager GameData;
    public float Typingspeed;
    private string SText;
    private bool IsChoiced;

    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<DataManager>();
        Typingspeed = 0.05f;
        StartCoroutine(TextEffect());

    }

    IEnumerator TextEffect()
    {      
        var data = GameData.TextData;

        foreach(var Curdata in data)
        {
            IsChoiced = false; 
            yield return Typing(Curdata.DialogList); // 본문 출력
            
            yield return new WaitForSeconds(0.5f);
            SetChoiceText(Curdata.LinkedTextID); // 선택지 출력
            GameManager.SetChoiceButton();

            while(!IsChoiced)// 선택하기 전까지 다음 텍스트를 불러오지 않음
            {
                yield return null;
            }
            SText = "";

        }
    }

    IEnumerator Typing(string text) // 타이핑 효과
    {
        foreach (var character in text)
        {
            SText += character;
            StoryText.text = SText;
            yield return new WaitForSeconds(Typingspeed);
        }
    }

    public void SetChoiceText(string LinkedTID) //  선택 버튼에 텍스트 띄우기
    {

        var ChoiceData = GameData.ChoiceText;
        var Select = ChoiceData.Where(data => data.ChoiceTID == LinkedTID);
        var SelectNum = 1;

        TextMeshProUGUI FirstText = FirstButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI SecondText = SecondButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI ThirdText = ThirdButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI FourthText = FourthButton.GetComponentInChildren<TextMeshProUGUI>();

        // 한 셀에 여러개의 LinkedTID 있는 경우 나눠야 함
        foreach (var a in Select)
        {
            if (SelectNum == 1)
            {
                FirstButton.SetActive(true);
                FirstText.text = a.Choicetext;
            }                
            else if (SelectNum == 2)
            {
                SecondButton.SetActive(true);
                SecondText.text = a.Choicetext;
            }              
            else if (SelectNum == 3)
            {
                ThirdButton.SetActive(true);
                ThirdText.text = a.Choicetext;
            }              
            else if (SelectNum == 4)
            {
                FourthButton.SetActive(true);
                FourthText.text = a.Choicetext;
            }
            SelectNum++;
        }
  
    }

    public string ResultText(string LinkedCID) // 결과 텍스트 출력
    {
        var Resultdata = GameData.ResultText;
        var Select = Resultdata.Where(data => data.ResultTID == LinkedCID);
        string ReturnData = "";

        foreach(var Result in Select)
        {
            ReturnData = Result.Result + "\n";
        }
        return ReturnData;
    }

    public void SelectChoice() // 선택 버튼 함수
    {

    }
}
