using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class UIManager : MonoBehaviour
{
     
    public Image InventoryPanel;
    public Image MapPanel;
    public Image StatPanel;
    public Image Background;
    public Image Sun;
    public GameObject EndingCreditPanel;
    public GameObject EndingData;
    public GameObject GiveUpPanel;
    public GameObject OptionPanel;
    [HideInInspector]
    public bool EnableInventoryPanel;
    public bool EnableMapPanel;
    public bool EnableStatPanel;
    private bool TimeSet;
    private float UITime;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI PlaytimeText;
    public GameStat GameStat;

    void Awake()
    {
        EnableInventoryPanel = false;
        EnableMapPanel = false;
        EnableStatPanel = false;
        TimeSet = false;
    }

    void Update()
    {
        //display ItemUI
        if (EnableInventoryPanel == true && TimeSet == true)
        {

            UITime += Time.deltaTime * 1500f;
            InventoryPanel.gameObject.SetActive(true);
            InventoryPanel.rectTransform.anchoredPosition = new Vector2(0, -400 + UITime);

            if (InventoryPanel.rectTransform.anchoredPosition.y >= 0)
            {
                TimeSet = false;
                InventoryPanel.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            }
        }
        else if (EnableInventoryPanel == false && TimeSet == true)
        {
            UITime += Time.deltaTime * 1500f;
            InventoryPanel.rectTransform.anchoredPosition = new Vector2(0, 0 - UITime);

            if (InventoryPanel.rectTransform.anchoredPosition.y <= -400)
            {
                TimeSet = false;
                InventoryPanel.gameObject.SetActive(false);
            }
        }
        else
            UITime = 0f;
    }

    //ItemUI ON/OFF
    public void SetUI(int UINum)
    {
        //set inventoryUI
        if (EnableInventoryPanel == false && UINum == 1)
        {
            EnableInventoryPanel = true;
            TimeSet = true;
        }
        else if (EnableInventoryPanel == true && UINum == 1)
        {
            EnableInventoryPanel = false;
            TimeSet = true;
        }

        if (EnableMapPanel == false && UINum == 2)
        {
            MapPanel.gameObject.SetActive(true);
            EnableMapPanel = true;
        }
        else if (EnableMapPanel == true && UINum == 2)
        {
            MapPanel.gameObject.SetActive(false);
            EnableMapPanel = false;
        }

        //set CharacterStatUI
        if (EnableStatPanel == false && UINum == 3)
        {
            StatPanel.gameObject.SetActive(true);
            EnableStatPanel = true;
        }
        else if (EnableStatPanel == true && UINum == 3)
        {
            StatPanel.gameObject.SetActive(false);
            EnableStatPanel = false;
        }
    }

    public IEnumerator Fade()
    {
        string text;
        string bold;
        if (GameStat.CurTime == "0")
        {
            text = "놀이공원에 아침이 찾아왔습니다.";
            bold = "<b>아침</b>";
            textComponent.text = text.Replace("아침", bold);
        }
            
        else if(GameStat.CurTime == "1")
        {
            text = "놀이공원에 점심이 찾아왔습니다.";
            bold = "<b>점심</b>";
            textComponent.text = text.Replace("점심", bold);
        }
        else if(GameStat.CurTime == "4")
        {
            text = "놀이공원에 저녁이 찾아왔습니다.";
            bold = "<b>저녁</b>";
            textComponent.text = text.Replace("저녁", bold);
        }
            

        float fadeIn = 0;
        while(fadeIn < 1.0f)
        {
            fadeIn += 0.05f;
            yield return new WaitForSeconds(0.1f);
            textComponent.color = new Color(0,0,0, fadeIn);
        }

        yield return new WaitForSeconds(2f);
        float fadeOut = 0;
        while (fadeOut < 1.0f)
        {
            fadeOut += 0.05f;
            yield return new WaitForSeconds(0.1f);
            textComponent.color = new Color(0, 0, 0, 1 - fadeOut);
        }
    }

    public void GoMain()
    {
        SceneManager.LoadScene("Lobby");
    }

    public IEnumerator SetEndingCredit()
    {
        EndingCreditPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
    }

    public void EndingResult()
    {
        PlaytimeText.text = GameStat.GetPlayTime();
        EndingCreditPanel.gameObject.SetActive(false);
        EndingData.gameObject.SetActive(true);
    }

    public void SetGiveUpPanel(int Select)
    {
        if(Select  == 0)
            GiveUpPanel.gameObject.SetActive(false);
        else
            GiveUpPanel.gameObject.SetActive(true);
    }

    public void SetOptionPane(int Select)
    {
        if (Select == 0)
            OptionPanel.gameObject.SetActive(false);
        else if(Select == 1)
            OptionPanel.gameObject.SetActive(true);
    }

    public void ChangeBackground() // 배경 변경
    {
        if(GameStat.CurTime == "1")
        {
            Sprite loadedImage = Resources.Load<Sprite>("Lunch");
            Background.sprite = loadedImage;
            Sun.rectTransform.anchoredPosition = new Vector2(173, -56);
        }
        else if(GameStat.CurTime == "4")
        {
            Sprite loadedImage = Resources.Load<Sprite>("Evening");
            Background.sprite = loadedImage;
            Sun.rectTransform.anchoredPosition = new Vector2(307, -108);
            Image aa = Background.GetComponent<Image>();
        }
    }

}
