using System;
using System.Collections;
using System.Collections.Generic;
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
    public TextData GameData;
    public bool isDialogEnd;
    public float Typingspeed;
    private string SText;
    private int CurTextNum;
    private int CurChoiceNum;
    private string CurID;

    

    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<TextData>();
        Typingspeed = 0.05f;
        StartCoroutine(TextEffect());
        CurTextNum = 0;
        CurChoiceNum = 0;
    }

    IEnumerator TextEffect()
    {
        SText = "";

         while(true)
        {
            if (CurTextNum == GameData.DialogList.Length - 1) //배열의 크기를 벗어나거나 현재 출력할 내용이 아니면 break
                break;
            else if (GameData.DialogPage[CurTextNum] != GameData.DialogPage[CurTextNum + 1])
                break;
            else
            {
                string a = GameData.DialogList;
                yield return Typing(a);
                CurTextNum++;
            }
        }
        yield return new WaitForSeconds(0.5f);
        GameManager.SetChoiceButton();
    }

    public void SetChoiceText()
    {
        TextMeshProUGUI FirstText = FirstButton.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI SecondText = SecondButton.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI ThirdText = ThirdButton.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI FourthText = FourthButton.GetComponent<TextMeshProUGUI>();
    }

    IEnumerator Typing(string text)
    {
        int CurrentChar = 0;
        int CharLength = text.Length;

        for(int a = 0; a < CharLength; a++)
        {
            SText += text[a];
            StoryText.text = SText;
            CurrentChar++;
            yield return new WaitForSeconds(Typingspeed);

            if (CurrentChar >= CharLength)
            {
                isDialogEnd = true;
                SText += '\n';
                yield break;
            }
        }
    }

}
