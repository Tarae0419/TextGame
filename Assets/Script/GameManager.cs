using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Image InventoryPanel;
    public Image MapPanel;
    public Image StatPanel;
    public bool EnableInventoryPanel;
    public bool EnableMapPanel;
    public bool EnableStatPanel;
    public TextMeshProUGUI NameText;
    public Character Character;
    private bool TimeSet;
    private float time;
    
    // Start is called before the first frame update

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

    // Update is called once per frame
    void Update()
    {
        SetCharacterStat();

        //display ItemUI
        if (EnableInventoryPanel == true && TimeSet == true)
        {

            time += Time.deltaTime * 1500f;
            InventoryPanel.gameObject.SetActive(true);
            InventoryPanel.rectTransform.anchoredPosition = new Vector2(0,-500 + time); 
            
            if (InventoryPanel.rectTransform.anchoredPosition.y >= 0)
            {
                TimeSet = false;
                InventoryPanel.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            }       
        }
        else if (EnableInventoryPanel == false && TimeSet == true)
        {
            time += Time.deltaTime * 1500f;
            InventoryPanel.rectTransform.anchoredPosition = new Vector2(0, 0 - time);

            if (InventoryPanel.rectTransform.anchoredPosition.y <= -500)
            {
                TimeSet = false;
                InventoryPanel.gameObject.SetActive(false);
            }
        }
        else
            time = 0f;
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
        else if(EnableInventoryPanel == true && UINum == 1)
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
        else if(EnableMapPanel == true && UINum == 2)
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
        else if(EnableStatPanel == true && UINum == 3)
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

    public void OpenMap()
    {

    }
}
