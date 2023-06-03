using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public List<TextCondition> TextCondition { get; set; }

    public List<StoryText> StoryText { get; set; }

    public List<ChoiceText> ChoiceText { get; set; }

    public List<ResultText> ResultText { get; set; }


}
