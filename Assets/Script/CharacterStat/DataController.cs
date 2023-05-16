using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataController : MonoBehaviour
{
    private static DataController instance = null;
    public LobbyData CharacterData;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void SetCharacterData(int Select)
    {
        switch (Select)
        {
            case 1: CharacterData = GetComponent<MomData>(); CharacterData.InitSetting(); break;
            case 2: CharacterData = GetComponent<RangerData>(); CharacterData.InitSetting(); break;
            case 3: CharacterData = GetComponent<PTJData>(); CharacterData.InitSetting(); break;
            case 4: CharacterData = GetComponent<PuppyData>(); CharacterData.InitSetting(); break;
        }
    }
}
