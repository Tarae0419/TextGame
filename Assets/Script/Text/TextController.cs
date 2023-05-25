using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Image ChoicePanel;
    public TextMeshProUGUI StoryText;
    public ScrollRect scrollRect;
    public GameObject FirstButton;
    public GameObject SecondButton;
    public GameObject ThirdButton;
    public GameObject FourthButton;
    public DataManager GameData;
    public float Typingspeed;
    private string SText;
    private string resultText;
    private bool IsChoiced;
    private IEnumerable<ChoiceText> Select;
    private ChoiceButtonUI ChoiceUI;


    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<DataManager>();
        Typingspeed = 0.05f;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {      
        var data = GameData.StoryText;

        foreach(var Curdata in data) //아침일 때 사이클
        {
            IsChoiced = false; 
            yield return TypingEffect(Curdata.DialogList); // 본문 출력
            
            yield return new WaitForSeconds(0.5f);
            ChoiceUI.SetChoiceText(); // 선택지 출력
            ChoicePanel.gameObject.SetActive(true);

            while (!IsChoiced)// 선택하기 전까지 다음 텍스트를 불러오지 않음
            {
                yield return null;
            }
            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // 결과 본문 출력
        }
    }

    IEnumerator TypingEffect(string text) // 타이핑 효과
    {
        foreach (var character in text)
        {
            SText += character;
            StoryText.text = SText;
            yield return new WaitForSeconds(Typingspeed);
        }
    }

    public void ResultText(string LinkedCID) // 결과 텍스트 출력
    {
        var Resultdata = GameData.ResultText;
        var Select = Resultdata.Where(data => data.ResultTID == LinkedCID);
        string ReturnData = "";

        foreach(var Result in Select)
        {
            ReturnData = Result.Result + "\n\n";
        }
        IsChoiced = true;
        resultText = ReturnData;
    }


    
}
