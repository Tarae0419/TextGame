using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public Image InventoryPanel;
    public Image MapPanel;
    public Image StatPanel;
    public Image TimerBar;
    public Image CurrentLocation;
    public bool EnableInventoryPanel;
    public bool EnableMapPanel;
    public bool EnableStatPanel;
    public TextMeshProUGUI NameText;
    public Character Character;
    private bool TimeSet;
    private bool Timer;
    private float UITime;
    private float TimerTime;


    void Awake()
    {
        EnableInventoryPanel = false;
        EnableMapPanel = false;
        EnableStatPanel = false;
        TimeSet = false;

    }
    void Start()
    {

    }

    void Update()
    {
        SetCharacterStat();


        PlayerLocation(Character.CurrentMapIndex);

        //display ItemUI
        if (EnableInventoryPanel == true && TimeSet == true)
        {

            UITime += Time.deltaTime * 1500f;
            InventoryPanel.gameObject.SetActive(true);
            InventoryPanel.rectTransform.anchoredPosition = new Vector2(0, -500 + UITime);

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

            if (InventoryPanel.rectTransform.anchoredPosition.y <= -500)
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
            TimerBar.fillAmount = 1 - TimerTime / 30;
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

        //set mapUI(²Ú ´­·¶À»¶§? or ¹öÆ°?)
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

    public void SetCharacterStat()
    {
        NameText.text = Character.Name;
        //other stat

    }

    public void SetTimer()
    {
        Timer = true;
    }

    // change player location in map
    public void PlayerLocation(int CurrentMapIndex)
    {
        switch (CurrentMapIndex)
        {
            case 1: CurrentLocation.rectTransform.anchoredPosition = new Vector2(125, -50); break;
            case 2: CurrentLocation.rectTransform.anchoredPosition = new Vector2(0, -50); break;
            case 3: CurrentLocation.rectTransform.anchoredPosition = new Vector2(0, 75); break;
            case 4: CurrentLocation.rectTransform.anchoredPosition = new Vector2(05, 200); break;
            case 5: CurrentLocation.rectTransform.anchoredPosition = new Vector2(-125, 200); break;
            case 6: CurrentLocation.rectTransform.anchoredPosition = new Vector2(0, 325); break;
            default: break;
        }
    }


}
