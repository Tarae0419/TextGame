using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    public TextAsset Asset;
    public Text TextDisplay;
    public float TypingSpeed = 0.15f;
    public int Page;
    [Tooltip("ด๋ป็")]
    public string[] TextArray;

    void Start()
    {
        TextDisplay.text = "aaa";
    }


    void Update()
    {

    }


}
[System.Serializable]

public class DialogueEvent
{
    public string name;

    public Vector2 line;
    public Dialogue[] dialogues;
}
