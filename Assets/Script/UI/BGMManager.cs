using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class BGMManager : MonoBehaviour
{
    public AudioClip MorningClip;
    public AudioClip LunchClip;
    public AudioClip EveningClip;
    public AudioClip ButtonClickClip;
    public AudioSource BGMSource;
    public AudioSource ButtonClickSource;

    private void Awake()
    {
        BGMSource = gameObject.AddComponent<AudioSource>();
        ButtonClickSource = gameObject.AddComponent<AudioSource>();

        Button[] buttons = FindObjectsOfType<Button>(); 

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(ButtonClickSound);
        }

        ButtonClickSource.clip = ButtonClickClip;
        BGMSource.loop = true;
        
        
    }

    public void StartMorningBGM()
    {
        BGMSource.clip = MorningClip;       
        BGMSource.Play();
    }
    public void StartLunchBGM()
    {
        BGMSource.Stop(); // ¾ÆÄ§BGM ²ô±â
        BGMSource.clip = LunchClip;
        BGMSource.Play();
    }
        
    public void StartEveningBGM()
    {
        BGMSource.Stop(); // Á¡½ÉBGM ²ô±â
        BGMSource.clip = EveningClip;
        BGMSource.Play();
    }

    public void ButtonClickSound()
    {
        ButtonClickSource.Play();

    }
}
