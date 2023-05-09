using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class DataParsing : MonoBehaviour
{
    private static DataParsing instance = null;
    public List<string> DialogPage;
    public List<string> DialogList;
    public List<string> ChoiceList;
    public List<string> ResultList;
    private string URL;
    public int DialogLength;
    public string DialogData;
    private string SheetRange;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        SheetRange = "A1:C8";
        StartCoroutine(DownloadScript(SheetRange));
    }
    public IEnumerator DownloadScript(string SheetRange)
    {
        URL = "https://docs.google.com/spreadsheets/d/1Xvwqn3W-MGwuaBay69MfeC6PW_1HYlPBnxwep8bgZlU/export?format=tsv&range=" + SheetRange;

        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
        DialogData = www.downloadHandler.text;
        print(DialogData);
        StoryParse();
    }

    public void StoryParse()
    {
        DialogLength = 0;
        string[] data = DialogData.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { '\t' });

            do
            {
                DialogPage.Add(row[1]);
                DialogList.Add(row[2]);
                DialogLength++;
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { '\t' });
                }
            } while (row[2].ToString() == "");
        }
    }
}
