using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.PlayerLoop.PreUpdate;

public class TextController : MonoBehaviour
{
    public TextMeshProUGUI StoryText;
    public ScrollRect scrollRect;
    public ChoiceButtonUI ChoiceUI;
    public ConditionChecker ConCheck;
    public DataManager GameData;
    public MapController MapController;
    public GameStat GameStat;
    [HideInInspector]
    public float Typingspeed;
    private string SText;
    private string resultText;
    private bool IsChoiced;

    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<DataManager>();
        ConCheck = gameObject.GetComponent<ConditionChecker>();
        Typingspeed = 0.05f;
        
    }
    public void Start()
    {
        StartCoroutine(GameStart());
    }

    IEnumerator TypingEffect(string text) // 타이핑 효과
    {
        foreach (var character in text)
        {
            SText += character;
            StoryText.text = SText;
            yield return new WaitForSeconds(Typingspeed);
        }
    }

    public void ResultText(string LinkedCID) // 결과 텍스트 출력
    {
        if (LinkedCID == null)
        {
            IsChoiced = true;
            return;
        }
            
        var Resultdata = GameData.ResultText;
        var Select = Resultdata.Where(data => data.ResultTID == LinkedCID);
        string ReturnData = "";

        foreach(var Result in Select)
        {
            ReturnData = Result.Result + "\n\n";
        }
        IsChoiced = true;
        resultText = ReturnData;
    }
    
    IEnumerator GameStart()
    {
        yield return Morning();
        yield return Lunch();
        yield return evening();
    }

    IEnumerator Morning()
    {
        var Morningdata = GameData.TextCondition.Join(GameData.StoryText, tc=>tc.TextID, st=>st.ConID, (tc, st)=> new { TextCondition=tc, StoryText=st  })
                          .Where(x=> x.TextCondition.Time == "0"); //아침 데이터 가져오기

        foreach (var Curdata in Morningdata) //아침일 때 사이클
        {
            IsChoiced = false;
            yield return TypingEffect(Curdata.StoryText.DialogList); // 본문 출력

            yield return new WaitForSeconds(0.5f);
            ChoiceUI.SetChoiceText(Curdata.StoryText.LinkedChoiceID); // 선택지 출력
            ChoiceUI.SetButton();

            while (!IsChoiced)// 선택하기 전까지 다음 텍스트를 불러오지 않음
            {
                yield return null;
            }
            int i = 0;
            MapController.MapUpdate(MapController.MorningMapName[i++]); //현재 플레이어 위치 변경
            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // 결과 본문 출력
        }
    }

    IEnumerator Lunch()
    {
        GameStat.CurTime = "1"; // 점심으로 변경
        MapController.MapUpdate("광장");

        var Lunchdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                        .Where(x => x.TextCondition.Time == "1" /*&& 강제텍스트*/); //점심 강제텍스트 가져오기

        foreach (var Curdata in Lunchdata) //점심 초반부분 사이클
        {
            IsChoiced = false;
            yield return TypingEffect(Curdata.StoryText.DialogList); // 본문 출력

            yield return new WaitForSeconds(0.5f);
            ChoiceUI.SetChoiceText(Curdata.StoryText.LinkedChoiceID); // 선택지 출력
            ChoiceUI.SetButton();

            while (!IsChoiced)// 선택하기 전까지 다음 텍스트를 불러오지 않음
            {
                yield return null;
            }
            int i = 0;
            MapController.MapUpdate(MapController.LunchMapName[i++]); //현재 플레이어 위치 변경
            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // 결과 본문 출력
        }


        for (int i = 0; i < 5; i++) // 점심 중반부분 사이클
        {
            var MapData = GameData.TextCondition.Where(x => x.Position == GameStat.CurPos && x.Time == GameStat.CurPos /*&& x.Map == "1"*/);

            MapController.SetMap("맵이름"); //맵 UI 활성화

        }

        //foreach() 점심 후반부분 사이클
    }

    IEnumerator evening()
    {
        GameStat.CurTime = "2"; // 저녁으로 변경

        var Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                         .Where(x => x.TextCondition.Time == "2"); // 저녁 강제텍스트 가져오기

        foreach (var Curdata in Dinnerdata) // 저녁 사이클
        {

        }

        var EndGame = "플레이 타임 : " + GameStat.GetPlayTime();
        yield return TypingEffect(EndGame);
    }
}
