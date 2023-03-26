using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public string[] _Dialogue;
    public TextMeshProUGUI TextDisplay;
    public float TypingSpeed = 0.15f;

    void Start()
    {
        TextDisplay.text = "aaa";
    }


    void Update()
    {

    }

    //typing effect
    IEnumerator TypingEffect()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i <= _Dialogue.Length; i++)
        {
            TextDisplay.text = _Dialogue[i].Substring(0, i);

            yield return new WaitForSeconds(TypingSpeed);
        }
    }
}
