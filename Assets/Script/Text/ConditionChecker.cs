using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    public DataManager GameData;
    public GameManager GMG;
    public IEnumerable<TextCondition> ConditionData;
    public IEnumerable<StoryText> StoryData;
    public IEnumerable<ChoiceText> ChoiceData;

    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<DataManager>();
        GMG = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ConditionCheck()
    {
        ConditionData = GameData.TextCondition.Where(Con => Con.Time == GMG.CurTime && Con.Location == GMG.CurPos); //적합한 Condition 열 가져오기
        StoryData = GameData.StoryText.Where(TID => TID.ConID == ConditionData.FirstOrDefault().TID); //적합한 Story 열 가져오기
        ChoiceData = GameData.ChoiceText.Where(CID => CID.ChoiceTID == StoryData.FirstOrDefault().LinkedTextID); //적합한 Choice 열 가져오기

    }
}
