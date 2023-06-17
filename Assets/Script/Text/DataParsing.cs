using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DataParsing : MonoBehaviour
{
    [HideInInspector]
    public DataManager DM;
    private static DataParsing instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        DM = gameObject.GetComponent<DataManager>();
        Parsing();
    }

    public void Parsing()
    {
        string ConditionPath = Path.Combine(Application.dataPath, "StreamingAssets/TextCondition.csv");

        using (var Condition = new StreamReader(ConditionPath))
        using (var csv = new CsvReader(Condition, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<TextCondition>();
            DM.TextCondition = records.ToList();
        }

        string ContentsPath = Path.Combine(Application.dataPath, "StreamingAssets/Contents.csv");

        using (var Text = new StreamReader(ContentsPath))
        using (var csv = new CsvReader(Text, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<StoryText>();
            DM.StoryText = records.ToList();
        }

        string EveningPath = Path.Combine(Application.dataPath, "StreamingAssets/Evening.csv");

        using (var Evening = new StreamReader(EveningPath))
        using (var csv = new CsvReader(Evening, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<EveningText>();
            DM.EveningText = records.ToList();
        }

        string ChoicePath = Path.Combine(Application.dataPath, "StreamingAssets/Choice.csv");

        using (var Choice = new StreamReader(ChoicePath))
        using (var csv = new CsvReader(Choice, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<ChoiceText>();
            DM.ChoiceText = records.ToList();
        }

        string ResultPath = Path.Combine(Application.dataPath, "StreamingAssets/Result.csv");

        using (var Result = new StreamReader(ResultPath))
        using (var csv = new CsvReader(Result, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<ResultText>();
            DM.ResultText = records.ToList();
        }
    }
}


