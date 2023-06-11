using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ChoiceButtonUI : MonoBehaviour
{
    public Image ChoicePanel;
    public GameObject FirstButton;
    public GameObject SecondButton;
    public GameObject ThirdButton;
    public GameObject FourthButton;
    public DataManager DataMGR;
    public TextController TC;
    private IEnumerable<string> LCIDData;

    private void Awake()
    {
        DataMGR = GameObject.Find("TextData").GetComponent<DataManager>();
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
                Debug.Log(a);
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
    }

    public void SelectChoice(int ButtonSelect) // 선택 버튼 함수
    {
        var ResultNum = 1;

        foreach (var a in LCIDData)
        {
            if (ResultNum == ButtonSelect)
            TC.ResultText(DataMGR.ChoiceText.Where(x => x.ChoiceTID == a).First().LinkedResultID);
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
}
