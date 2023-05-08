using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class TextManager : MonoBehaviour
{
    private string[] TextData;
    public TextMeshProUGUI StoryText;
    public GameObject FirstButton;
    public GameObject SecondButton;
    public GameObject ThirdButton;
    public GameObject FourthButton;
    public GameManager GameManager;
    public bool isDialogEnd;
    public float Typingspeed;
    private string URL;
    public List<string> Dialog;
    public int DialogLength;
    private string DialogData;
    private string SText;
    private string SheetRange;
    

    private void Awake()
    {
        Typingspeed = 0.05f;
        SheetRange =  "A1:C8";
        
        StartCoroutine(TextEffect());
    }

    IEnumerator DownloadScript()
    {
        URL = "https://docs.google.com/spreadsheets/d/1Xvwqn3W-MGwuaBay69MfeC6PW_1HYlPBnxwep8bgZlU/export?format=tsv&range=" + SheetRange;
        
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        DialogData = www.downloadHandler.text;
        print(DialogData);
        this.Dialog = Parse();
    }

    IEnumerator TextEffect()
    {
        yield return DownloadScript();
        SText = "";
        for (int i = 0; i < DialogLength; i++)
        {
            string a = this.Dialog[i];
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

    public List<string> Parse()
    {
        DialogLength = 0;
        List<string> context = new List<string>();
        string[] data = DialogData.Split(new char[] { '\n' });

        for(int i = 1; i <data.Length;)
        {
            string[] row = data[i].Split(new char[] { '\t' });
            
            do 
            {
                context.Add(row[2]);
                DialogLength++;
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { '\t' });
                }
            } while (row[2].ToString() == "");
        }

        return context;
    }
}
