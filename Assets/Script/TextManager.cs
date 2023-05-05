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
    public bool isDialogEnd;
    public float Typingspeed;
    public int index;
    private string URL;
    public List<string> Dialog;
    public int DialogLength;
    private string DialogData;
    private string SText;

    private void Awake()
    {
        index = 0;
        Typingspeed = 0.05f;
        URL = "https://docs.google.com/spreadsheets/d/1Xvwqn3W-MGwuaBay69MfeC6PW_1HYlPBnxwep8bgZlU/export?format=tsv";
        StartCoroutine(DownloadScript());
        
    }

    IEnumerator DownloadScript()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        DialogData = www.downloadHandler.text;
        this.Dialog = Parse();
        StartCoroutine(TextEffect());  
    }

    IEnumerator TextEffect()
    {
        SText = "";
        for (int i = 0; i < DialogLength; i++)
        {
            string a = this.Dialog[i];
            //StartCoroutine(Typing(a));
            yield return Typing(a);
        }  
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
