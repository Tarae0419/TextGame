using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    private string[] TextData;
    public TextMeshProUGUI StoryText;
    public bool isDialogEnd;
    public float speed;
    public int index;



    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void TextEffect()
    {
        string a = "Hello world";
        char[] a2 = a.Substring(0, a.Length - 1).ToCharArray();
    }

}
