using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public DataManager GameData;
    public MapController MapController;
    public GameStat GameStat;
    public BGMManager BGMManager;
    public UIManager UIManager;
    [HideInInspector]
    public float Typingspeed;
    private string SText;
    private string resultText;
    private bool IsChoiced;
    public bool HaveClue;
    public bool IsEnd;

    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<DataManager>();
        Typingspeed = 0.00001f;
        
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
    
    IEnumerator GameStart() //시작 함수
    {
        BGMManager.BGMSource.Stop();
        //yield return StartFade();
        //yield return Morning();
        //ClearText();
        //BGMManager.BGMSource.Stop();
        //yield return LunchBeginning();
        //yield return LunchMiddle();
        //ClearText();
        BGMManager.BGMSource.Stop();
        yield return Evening();
        yield return EveningMiddle();
        ClearText();   
        yield return UIManager.SetEndingCredit();
        UIManager.EndingResult();
    }

    IEnumerator StartFade()
    {
        UIManager.textComponent.gameObject.SetActive(true);
        yield return UIManager.Fade();
        yield return new WaitForSeconds(1f);
        UIManager.textComponent.gameObject.SetActive(false);
    }

    IEnumerator Morning()
    {
        BGMManager.StartMorningBGM(); // 아침 BGM 시작

        var Morningdata = GameData.TextCondition.Join(GameData.StoryText, tc=>tc.TextID, st=>st.ConID, (tc, st)=> new { TextCondition=tc, StoryText=st  })
                          .Where(x=> x.TextCondition.Time == "0"); //아침 데이터 가져오기
        foreach (var Curdata in Morningdata) //아침일 때 사이클
        {
            IsChoiced = false;
            GameStat.IsMapChoiced = false;

            yield return TypingEffect(Curdata.StoryText.DialogList); // 본문 출력

            yield return new WaitForSeconds(0.5f);
            Debug.Log(Curdata.StoryText.LinkedChoiceID);
            ChoiceUI.SetChoiceText(Curdata.StoryText.LinkedChoiceID); // 선택지 출력
            ChoiceUI.SetButton();

            yield return WaitChoiceSelect();
            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // 결과 본문 출력
            if (Curdata.TextCondition.Map == "1")
            {
                MapController.SetMap(Curdata.TextCondition.MapPosition);
                yield return WaitMapSelect();
            }           
        }
    }

    IEnumerator LunchBeginning()
    {
        BGMManager.StartLunchBGM(); // 점심 BGM 시작


        GameStat.CurTime = "1"; // 점심으로 변경
        MapController.MapUpdate("광장");

        UIManager.ChangeBackground(); //이미지 변경

        yield return StartFade();

        var Lunchdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                        .Where(x => x.TextCondition.Time == "1"); //점심 강제텍스트 가져오기

        foreach (var Curdata in Lunchdata) //점심 초반부분 사이클
        {
            IsChoiced = false;
            
            yield return TypingEffect(Curdata.StoryText.DialogList); // 본문 출력

            yield return new WaitForSeconds(0.5f);
            ChoiceUI.SetChoiceText(Curdata.StoryText.LinkedChoiceID); // 선택지 출력
            ChoiceUI.SetButton();

            yield return WaitChoiceSelect();

            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // 결과 본문 출력
            if (Curdata.TextCondition.Map == "1")
            {
                GameStat.IsMapChoiced = false;

                if (Curdata.TextCondition.MapPosition == "0")
                {
                    MapController.SetButton();
                    yield return WaitMapSelect();
                }
                else
                {
                    MapController.SetMap(Curdata.TextCondition.MapPosition);
                    yield return WaitMapSelect();
                }                
            }          
        }
    }

    IEnumerator LunchMiddle()
    {
        MapController.MapUpdate("광장");

        for (int i = 0; i < 5; i++) // 점심 중반부분 사이클
        {
            GameStat.CurTime = "2";

            var MapData = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                        .Where(x => x.TextCondition.Time == "2" && x.TextCondition.Position == GameStat.CurPos);


            var Curdata = MapData.First();
                         
            IsChoiced = false;
               
            GameStat.IsMapChoiced = false;

            yield return TypingEffect(Curdata.StoryText.DialogList); // 본문 출력
               
            yield return new WaitForSeconds(0.5f);
                 
            if (Curdata.TextCondition.Map == "0")
            {
                yield return ClueCylce(i);
            }            
            else    
            {
                SText = "";              
                scrollRect.verticalNormalizedPosition = 1f;              
                MapController.SetButton();               
                yield return WaitMapSelect();           
            }
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator ClueCylce(int turn)
    {
        GameStat.CurTime = "3";

        var ClueData = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                        .Where(x => x.TextCondition.Time == "3" && x.TextCondition.Position == GameStat.CurPos && x.TextCondition.NPCType == GameStat.CheckNPC(turn));

        foreach (var b in ClueData)
        {
            IsChoiced = false;
            GameStat.IsMapChoiced = false;

            yield return TypingEffect(b.StoryText.DialogList); // 본문 출력

            yield return new WaitForSeconds(0.5f);
            ChoiceUI.SetChoiceText(b.StoryText.LinkedChoiceID); // 선택지 출력
            ChoiceUI.SetButton();

            yield return WaitChoiceSelect();
            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // 결과 본문 출력
            MapController.SetButton();
            yield return WaitMapSelect();
        }
    }

    IEnumerator Evening()
    {
        BGMManager.StartEveningBGM(); // 저녁 BGM 시작

        GameStat.CurTime = "4";
        MapController.MapUpdate("광장");

        UIManager.ChangeBackground(); //이미지 변경
        yield return StartFade();

        var LunchLatedata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                         .Where(x => x.TextCondition.Time == "4"); //저녁 초반 데이터 가져오기

        foreach (var Curdata in LunchLatedata)
        {
            IsChoiced = false;
            GameStat.IsMapChoiced = false;

            yield return TypingEffect(Curdata.StoryText.DialogList); // 본문 출력

            yield return new WaitForSeconds(0.5f);
            if (Curdata.StoryText.ConID == "Con_50" && HaveClue == false)
            {
                ChoiceUI.SetChoiceText("Cho_46");
                ChoiceUI.SetButton();
                yield return WaitChoiceSelect();
                SText = "";
                scrollRect.verticalNormalizedPosition = 1f;
                yield return TypingEffect(resultText);
                yield return new WaitForSeconds(3f);
                yield break;
            }
            else
            {
                ChoiceUI.SetChoiceText(Curdata.StoryText.LinkedChoiceID); // 선택지 출력
                ChoiceUI.SetButton();
            }
            

            yield return WaitChoiceSelect();
            SText = "";
            scrollRect.verticalNormalizedPosition = 1f;
            yield return TypingEffect(resultText); // 결과 본문 출력
            if (Curdata.TextCondition.Map == "1")
            {             
                MapController.SetMap(Curdata.TextCondition.MapPosition);
                yield return WaitMapSelect();
            }

        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator EveningMiddle()
    {

        if (HaveClue == false || IsEnd == true)
            yield break;

        GameStat.CurTime = "5";

        var Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                         .Where(x => x.TextCondition.Time == "5"); // 전투 텍스트 가져오기

        foreach (var Curdata in Dinnerdata) // 저녁 사이클
        {

        }

    }

    IEnumerator Ending()
    {
        var EndingData = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                         .Where(x => x.TextCondition.Time == "6"); // 엔딩 텍스트 가져오기

        yield break;
    }

    IEnumerator WaitMapSelect()
    {
        while (!GameStat.IsMapChoiced)// 맵을 선택하기 전까지 다음 텍스트를 불러오지 않음
        {
            yield return null;
        }
    }

    IEnumerator WaitChoiceSelect()
    {
        while (!IsChoiced)// 선택하기 전까지 다음 텍스트를 불러오지 않음
        {
            yield return null;
        }
    }

    public void ClearText()
    {
        StoryText.text = "";
    }
}
