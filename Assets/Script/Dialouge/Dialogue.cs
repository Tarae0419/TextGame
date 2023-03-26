using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    public TextAsset DialogueAsset;
    public Text TextDisplay;
    public float TypingSpeed = 0.15f;
    [Tooltip("ด๋ป็")]
    public string[] TextArray;

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
        for (int i = 0; i <= DialogueAsset.text.Length; i++)
        {
            TextDisplay.text = DialogueAsset.text.Substring(0, i);

            yield return new WaitForSeconds(TypingSpeed);
        }
    }
}
[System.Serializable]

public class DialogueEvent
{
    public string name;

    public Vector2 line;
    public Dialogue[] dialogues;
}
