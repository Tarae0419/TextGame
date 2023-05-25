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
        using (var Text = new StreamReader("D:\\P1 git\\P1\\Assets\\Resources\\Text.csv"))
        using (var csv = new CsvReader(Text, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<StoryText>();
            DM.StoryText = records.ToList();
        }

        using (var Choice = new StreamReader("D:\\P1 git\\P1\\Assets\\Resources\\Choice.csv"))
        using (var csv = new CsvReader(Choice, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<ChoiceText>();
            DM.ChoiceText = records.ToList();
        }

        using (var Result = new StreamReader("D:\\P1 git\\P1\\Assets\\Resources\\Result.csv"))
        using (var csv = new CsvReader(Result, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<ResultText>();
            DM.ResultText = records.ToList();
        }
    }
}


