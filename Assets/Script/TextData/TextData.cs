using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextData : DataParsing
{

    public List<string> DialogData;

    public List<string> Page;

    protected override void Awake()
    {
        base.Awake();
        Page = Parsing(1, "Text");
        DialogData = Parsing(2, "Text");
    }
}