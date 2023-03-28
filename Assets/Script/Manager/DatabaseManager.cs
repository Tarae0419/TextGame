using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] const string TsvFile = "https://docs.google.com/spreadsheets/d/1Xvwqn3W-MGwuaBay69MfeC6PW_1HYlPBnxwep8bgZlU/edit?usp=sharing";
    Item item;
    void Awake()
    {
 
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest www = UnityWebRequest.Get(TsvFile);
        yield return www.SendWebRequest();
       Splitdata(www.downloadHandler.text);
    }

    void Splitdata(string TSVFile)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();

        string[] Row = TSVFile.Split("\n");
        int RowSize = Row.Length;
        int ColumnSize = Row[0].Split("\t").Length;

        for(int i = 0; i < RowSize; i++)
        {
            string[] Column = Row[i].Split("\t");
            for(int j = 0; j < ColumnSize; j++)
            {

            }
        }
    }
}

