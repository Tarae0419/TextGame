using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    private string[] TextData;
    public TextMeshProUGUI StoryText;
    public GameObject FirstButton;
    public GameObject SecondButton;
    public GameObject ThirdButton;
    public GameObject FourthButton;
    public GameManager GameManager;
    public DataParsing GameData;
    public bool isDialogEnd;
    public float Typingspeed;
    private string SText;

    

    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<DataParsing>();
        Typingspeed = 0.05f;
        StartCoroutine(TextEffect());
    }

    IEnumerator TextEffect()
    {
        SText = "";
        for (int i = 0; i < GameData.DialogLength; i++)
        {
            string a = GameData.DialogList[i];
            //StartCoroutine(Typing(a));
            yield return Typing(a);
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
