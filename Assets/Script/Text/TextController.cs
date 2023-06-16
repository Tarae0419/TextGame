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
    public bool HaveSight;
    public bool HaveConfidence;
    public bool HaveDetection;
    public bool HaveSense;
    public bool HaveDog;
    public string PreviousResult;

    private void Awake()
    {
        GameData = GameObject.Find("TextData").GetComponent<DataManager>();
        Typingspeed = 0.000001f;
        HaveClue = true;


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
        //BGMManager.BGMSource.Stop();
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
        yield return Ending();
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
            yield return new WaitForSeconds(1f);
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
            yield return new WaitForSeconds(1f);
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

        GameStat.CurTime = "2";

        for (int i = 0; i < 5; i++) // 점심 중반부분 사이클
        {
            GameStat.CheckNPC(i);

            if (HaveClue == true && GameStat.CurPos == "광장")
                yield break;

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
            yield return new WaitForSeconds(1f);
            if (HaveClue == true)
            {
                string clueText = "<b>단서</b>를 얻었다.\n\n";
                SText = "";
                scrollRect.verticalNormalizedPosition = 1f;
                yield return TypingEffect(clueText);
                
                yield return new WaitForSeconds(2f);
            }
                
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
            yield return new WaitForSeconds(1f);
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
        SText = "";
        int ConIDnum= 54;
        if (HaveClue == false || IsEnd == true)
            yield break;

        GameStat.CurTime = "5";
        

        while(true)
        {
            Debug.Log(PreviousResult);

            var Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                         .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == ConIDnum); // 전투 텍스트 가져오기

            if (PreviousResult == "Res_45")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID >= 54);
            }
            else if (PreviousResult == "Res_46" || PreviousResult == "Res_47" || PreviousResult == "Res_48" || PreviousResult == "Res_49" || PreviousResult == "Res_50" || PreviousResult == "Res_51")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 58);
            }
            else if (PreviousResult == "Res_52" || PreviousResult == "Res_54")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 63);
            }
            else if (PreviousResult == "Res_53" || PreviousResult == "Res_66")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 59);
            }
            else if (PreviousResult == "Res_56")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 61);
            }
            else if (PreviousResult == "Res_59")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 84);
            }
            else if (PreviousResult == "Res_64")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 87);
            }
            else if (PreviousResult == "Res_65")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 85);
            }
            else if (PreviousResult == "Res_67")
            {
                break;
            }
            else if (PreviousResult == "Res_55" || PreviousResult == "Res_58")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 68);
            }
            else if (PreviousResult == "Res_63" || PreviousResult == "Res_68" || PreviousResult == "Res_69" || PreviousResult == "Res_70")
            {
                Dinnerdata = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                             .Where(x => x.TextCondition.Time == "5" && x.TextCondition.ConID == 55);
            }


            foreach (var Curdata in Dinnerdata) // 저녁 사이클
            {
                Debug.Log(Curdata.TextCondition.ConID);
                ConIDnum = Curdata.TextCondition.ConID;
                IsChoiced = false;

                yield return TypingEffect(Curdata.StoryText.DialogList);

                if (Curdata.StoryText.ConID == "Con_58" && HaveSense == true)
                {
                    ChoiceUI.SetChoiceText("Cho_59,Cho_61");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_58" && HaveSense == false)
                {
                    ChoiceUI.SetChoiceText("Cho_59,Cho_60");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_59" && HaveDetection == false)
                {
                    ChoiceUI.SetChoiceText("Cho_62,Cho_63");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_55" && HaveDog == false)
                {
                    ChoiceUI.SetChoiceText("Cho_53,Cho_54");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_56" && HaveDog == false)
                {
                    ChoiceUI.SetChoiceText("Cho_55,Cho_56");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_57" && HaveDog == false)
                {
                    ChoiceUI.SetChoiceText("Cho_57,Cho_58");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_63" && HaveDetection == false)
                {
                    ChoiceUI.SetChoiceText("Cho_62,Cho_69");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_68" && HaveSight == false)
                {
                    ChoiceUI.SetChoiceText("Cho_76,Cho_77");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else if (Curdata.StoryText.ConID == "Con_84" && HaveDog == false)
                {
                    ChoiceUI.SetChoiceText("Cho_81,Cho_82");
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                else
                {
                    ChoiceUI.SetChoiceText(Curdata.StoryText.LinkedChoiceID); // 선택지 출력
                    ChoiceUI.SetButton();
                    yield return WaitChoiceSelect();
                    SText = "";
                    scrollRect.verticalNormalizedPosition = 1f;
                    yield return TypingEffect(resultText);
                }
                yield return new WaitForSeconds(1f);
                ConIDnum++;
                break;
            }
        }

    }

    IEnumerator Ending()
    {
        SText = "";

        if (HaveClue == true)
        {
            var EndingData = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                         .Where(x => x.TextCondition.Time == "6"); // 엔딩 텍스트 가져오기

            foreach (var CurData in EndingData)
            {

                IsChoiced = false;
                yield return TypingEffect(CurData.StoryText.DialogList); // 본문 출력

                yield return new WaitForSeconds(0.5f);

                ChoiceUI.SetChoiceText(CurData.StoryText.LinkedChoiceID); // 선택지 출력
                ChoiceUI.SetButton();
                yield return WaitChoiceSelect();
                SText = "";
            }
        }
        else
        {
            var EndingData = GameData.TextCondition.Join(GameData.StoryText, tc => tc.TextID, st => st.ConID, (tc, st) => new { TextCondition = tc, StoryText = st })
                         .Where(x => x.TextCondition.Time == "6" && x.TextCondition.ConID >= 90); // 엔딩 텍스트 가져오기

            foreach (var CurData in EndingData)
            {

                IsChoiced = false;
                yield return TypingEffect(CurData.StoryText.DialogList); // 본문 출력

                yield return new WaitForSeconds(0.5f);

                ChoiceUI.SetChoiceText(CurData.StoryText.LinkedChoiceID); // 선택지 출력
                ChoiceUI.SetButton();
                yield return WaitChoiceSelect();
                SText = "";
            }
        }
        yield return new WaitForSeconds(2f);
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
