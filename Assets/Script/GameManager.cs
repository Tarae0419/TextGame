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
    public TMP_Dropdown fontSizeDropdown;
    void Start()
    {
        SetFont();
    }

    public void SetFont()
    {
        var FontSize = PlayerPrefs.GetInt("FontSize");
        Debug.Log(FontSize);

        contents.fontSize = PlayerPrefs.GetInt("FontSize");
        Button1.fontSize = PlayerPrefs.GetInt("FontSize") - 1;
        Button2.fontSize = PlayerPrefs.GetInt("FontSize") - 1;
        Button3.fontSize = PlayerPrefs.GetInt("FontSize") - 1;
        Button4.fontSize = PlayerPrefs.GetInt("FontSize") - 1;

        for (int i = 0; i < fontSizeDropdown.options.Count; i++)
        {
            string optionText = fontSizeDropdown.options[i].text;

            if (int.TryParse(optionText, out int fontSize) && fontSize == FontSize)
            {
                Debug.Log(i);
                fontSizeDropdown.value = i;
                break;
            }
        }
    }

}
