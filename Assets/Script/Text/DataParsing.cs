using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class DataParsing : MonoBehaviour
{
    private void Awake()
    {
        Parsing();
    }

    public void Parsing()
    {
        using (var Text = new StreamReader("E:\\P1 git\\P1\\Assets\\Resources\\Text.csv"))
        using (var csv = new CsvReader(Text, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<TextData>();

            records.Where(record => record.DialogList == "1");
        }

        using (var Choice = new StreamReader("E:\\P1 git\\P1\\Assets\\Resources\\Choice.csv"))
        using (var csv = new CsvReader(Choice, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<ChoiceText>();
        }

        using (var Result = new StreamReader("E:\\P1 git\\P1\\Assets\\Resources\\Result.csv"))
        using (var csv = new CsvReader(Result, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<ResultText>();
        }
    }
}


