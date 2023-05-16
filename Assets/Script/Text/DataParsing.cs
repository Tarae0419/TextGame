using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataParsing : MonoBehaviour
{
    private int DialogLength;
    public TextData DataManager;

    private void Awake()
    {
        DataManager = gameObject.GetComponent<TextData>();

        DataManager.DialogPage = Parsing(1, "Text");
        DataManager.DialogList = Parsing(2, "Text");
    }

    public List<string> Parsing(int Column ,string ExcelData) //추출할 CSV 파일과 필요한 열 입력
    {
        DialogLength = 0;
        List<string> ResultData = new List<string>();
        TextAsset DialogData = Resources.Load<TextAsset>(ExcelData);

        string[] data = DialogData.text.Split(new char[] { '\n' });

        for (int i = 0; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
            ResultData.Add(row[Column]);
            DialogLength++;
        }
        return ResultData; //저장한 값 반환
    }
}
