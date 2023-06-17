using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class ChoiceButtonUI : MonoBehaviour
{
    public Image ChoicePanel;
    public GameObject FirstButton;
    public GameObject SecondButton;
    public GameObject ThirdButton;
    public GameObject FourthButton;
    private RectTransform First;
    private RectTransform Second;
    private RectTransform Third;
    private RectTransform Fourth;
    public DataManager DataMGR;
    public TextController TC;
    private IEnumerable<string> LCIDData;

    private void Awake()
    {
        DataMGR = GameObject.Find("TextData").GetComponent<DataManager>();
        First = FirstButton.GetComponent<RectTransform>();
        Second = SecondButton.GetComponent<RectTransform>();
        Third = ThirdButton.GetComponent<RectTransform>();
        Fourth = FourthButton.GetComponent<RectTransform>();
    }

    public void SetChoiceText(string LCID) //  선택 버튼에 텍스트 띄우기
    {
        TextMeshProUGUI FirstText = FirstButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI SecondText = SecondButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI ThirdText = ThirdButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI FourthText = FourthButton.GetComponentInChildren<TextMeshProUGUI>();

        LCIDData = LCID.Split(',');

        var SelectNum = 1;

        foreach (var a in LCIDData) // 한 셀에 여러개의 LinkedTID 있는 경우 나눠야 함
        {
            if (SelectNum == 1)
            {
                FirstButton.SetActive(true);
                FirstText.text = DataMGR.ChoiceText.Where(x=>x.ChoiceTID == a).First().Choicetext;
            }
            else if (SelectNum == 2)
            {
                SecondButton.SetActive(true);
                SecondText.text = DataMGR.ChoiceText.Where(x => x.ChoiceTID == a).First().Choicetext;
            }
            else if (SelectNum == 3)
            {
                ThirdButton.SetActive(true);
                ThirdText.text = DataMGR.ChoiceText.Where(x => x.ChoiceTID == a).First().Choicetext;
            }
            else if (SelectNum == 4)
            {
                FourthButton.SetActive(true);
                FourthText.text = DataMGR.ChoiceText.Where(x => x.ChoiceTID == a).First().Choicetext;
            }
            SelectNum++;
        }
        SetButtonPosition(SelectNum - 1);
    }

    public void SelectChoice(int ButtonSelect) // 선택 버튼 함수
    {
        var ResultNum = 1;

        foreach (var a in LCIDData)
        {

            var LInkedRID = DataMGR.ChoiceText.Where(x => x.ChoiceTID == a).First().LinkedResultID;
            if (ResultNum == ButtonSelect)
            {
                
                TC.PreviousResult = LInkedRID;
                Debug.Log("버튼" + TC.PreviousResult);
                TC.ResultText(LInkedRID);
                if (LInkedRID == "Res_33" || LInkedRID == "Res_37" || LInkedRID == "Res_40")
                    TC.HaveClue = true;
                if(LInkedRID == "Res_43")
                    TC.IsEnd = true;
                GetItem(LInkedRID);
                break;
            }            
            ResultNum++;
        }
        ButtonOff();
    }

    public void ButtonOff() // 버튼 초기화
    {
        ChoicePanel.gameObject.SetActive(false);

        FirstButton.SetActive(false);
        SecondButton.SetActive(false);
        ThirdButton.SetActive(false);
        FourthButton.SetActive(false);
    }

    public void SetButton() // 버튼 띄우기
    {
        ChoicePanel.gameObject.SetActive(true);
    }

    public void SetButtonPosition(int ButtonCount) // 버튼 위치 설정
    {
        if (ButtonCount == 1)
            First.anchoredPosition = new Vector2(13, -727);
        else if (ButtonCount == 2)
        {
            First.anchoredPosition = new Vector2(13, -667);
            Second.anchoredPosition = new Vector2(13, -727);
        }
        else if(ButtonCount == 3) 
        {
            First.anchoredPosition = new Vector2(13, -607);
            Second.anchoredPosition = new Vector2(13, -667);
            Third.anchoredPosition = new Vector2(13, -727);
        }
        else if(ButtonCount == 4)
        {
            First.anchoredPosition = new Vector2(13, -547);
            Second.anchoredPosition = new Vector2(13, -607);
            Third.anchoredPosition = new Vector2(13, -667);
            Fourth.anchoredPosition = new Vector2(13, -727);
        }  
    }

    public void GetItem(string LID)
    {
        if (LID == "Res_46")
            TC.HaveSight = true;
        if (LID == "Res_49")
            TC.HaveConfidence = true;
        if (LID == "Res_51")
            TC.HaveSense = true;
        if (LID == "Res_52" && TC.HaveSight == false)
            TC.HaveDetection = true;
        if (LID == "Res_58")
            TC.HaveDog = true;

    }
}
