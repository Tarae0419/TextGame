using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] const string TsvFile = "https://docs.google.com/spreadsheets/d/1Xvwqn3W-MGwuaBay69MfeC6PW_1HYlPBnxwep8bgZlU/edit?usp=sharing";
    public TextAsset Asset;
    void Awake()
    {
 
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest www = UnityWebRequest.Get(TsvFile);
        yield return www.SendWebRequest();
        Splitdata(Asset);
    }

    void Splitdata(TextAsset TSVFile)
    {
        string[] Row = TSVFile.text.Split(new char[] { '\n' });
        print(Row[0]);
        int RowSize = Row.Length;
        int ColumnSIze = Row[0].Split(',').Length;


        for(int i = 0; i < RowSize; i++)
        {
            string[] Column = Row[i].Split(',');
            for (int j = 0; j < ColumnSIze; j++)
            {
                print(Column[j]);
            }
        }
           
        
    }

    public Dialogue[] GetDialogue(int StartNum, int EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();

        for(int i = 0; i <= EndNum - StartNum; i++)
        {
            //dialogueList.Add();
        }
        return dialogueList.ToArray();
    }
}

