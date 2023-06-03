using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class Item : MonoBehaviour
{
    public List<string> ItemArray;
    public TextMeshProUGUI FirstColumText;
    public TextMeshProUGUI SecondColumText;
    public TextMeshProUGUI ThirdColumText;
    private int ColumnCount;

    private void Awake()
    {
        ColumnCount = 0;
    }

    void Start()
    {
        ItemArray.Add("권총");
        ItemArray.Add("개껌");
        ItemArray.Add("고구마");
        ItemArray.Add("손목시계");
        ItemArray.Add("신문");
        ItemArray.Add("안경");
        ItemArray.Add("양념치킨");
        ItemArray.Add("물"); 
        ItemArray.Add("휴지");
        PrintItem();
    }

    public void PrintItem()
    {
        FirstColumText.text = "";
        SecondColumText.text = "";
        ThirdColumText.text = "";

        foreach (var Item in ItemArray)
        {
            switch (ColumnCount)
            {
                case 0: FirstColumText.text = FirstColumText.text + Item + "\n"; break;
                case 1: SecondColumText.text = SecondColumText.text + Item + "\n"; break;
                case 2: ThirdColumText.text = ThirdColumText.text + Item + "\n"; break;
            }
            ColumnCount = (ColumnCount + 1) % 3;
        }
    }

}
