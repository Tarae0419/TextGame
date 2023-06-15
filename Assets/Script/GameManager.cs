using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI contents;
    public TextMeshProUGUI Button1;
    public TextMeshProUGUI Button2;
    public TextMeshProUGUI Button3;
    public TextMeshProUGUI Button4;
    void Start()
    {
        SetFont();
    }

    public void SetFont()
    {
        contents.fontSize = PlayerPrefs.GetInt("FontSize");
        Button1.fontSize = PlayerPrefs.GetInt("FontSize") - 1;
        Button2.fontSize = PlayerPrefs.GetInt("FontSize") - 1;
        Button3.fontSize = PlayerPrefs.GetInt("FontSize") - 1;
        Button4.fontSize = PlayerPrefs.GetInt("FontSize") - 1;
    }

}
