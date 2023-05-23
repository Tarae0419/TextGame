using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
     
    public Image InventoryPanel;
    public Image MapPanel;
    public Image StatPanel;
    public Image TimerBar;
    public Camera MainCamera;
    public TextController TMG;
    [HideInInspector]
    public bool EnableInventoryPanel;
    public bool EnableMapPanel;
    public bool EnableStatPanel;
    public int GameTurn;
    public int Chapter;
    private bool TimeSet;
    private bool Timer;
    private float UITime;
    private float TimerTime;

    void Awake()
    {
        TMG = GetComponent<TextController>();
        EnableInventoryPanel = false;
        EnableMapPanel = false;
        EnableStatPanel = false;
        TimeSet = false;
        GameTurn = 1;
        Chapter = 1;

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

        // set timer
        if (Timer == true)
        {
            TimerTime += Time.deltaTime;
            TimerBar.fillAmount = 1 - TimerTime / 2;
            if (TimerBar.fillAmount == 0)
            {
                Timer = false;
            }
        }
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

        //set mapUI(꾹 눌렀을때? or 버튼?)
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

    public void SetTimer()
    {
        Timer = true;
    }

    public void TurnIncrease()
    {
        //서버 시간 반영
        GameTurn++;
    }

}
