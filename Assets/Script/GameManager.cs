using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image InventoryUI;
    public bool EnableInventory;
    private bool TimeSet;
    private float time;
    
    // Start is called before the first frame update

    void Awake()
    {
        EnableInventory = false;
        TimeSet = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //display ItemUI
        if (EnableInventory == true && TimeSet == true)
        {
            time += Time.deltaTime * 1500f;
            InventoryUI.gameObject.SetActive(true);
            InventoryUI.rectTransform.anchoredPosition = new Vector2(0,-500 + time); 
            
            if (InventoryUI.rectTransform.anchoredPosition.y >= 0)
            {
                TimeSet = false;
                InventoryUI.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            }       
        }
        else if (EnableInventory == false && TimeSet == true)
        {
            time += Time.deltaTime * 1500f;
            InventoryUI.rectTransform.anchoredPosition = new Vector2(0, 0 - time);

            if (InventoryUI.rectTransform.anchoredPosition.y <= -500)
            {
                TimeSet = false;
                InventoryUI.gameObject.SetActive(false);
            }
        }
        else
            time = 0f;
    }

    //ItemUI ON/OFF
    public void SetInventoryUI()
    {
        if (EnableInventory == false)
        {
            EnableInventory = true;
            TimeSet = true; 
        }
        else
        {
            EnableInventory = false;
            TimeSet = true;
        }
    }
}
