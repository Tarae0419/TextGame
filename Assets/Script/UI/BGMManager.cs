using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Threading.Tasks;

public class BGMManager : MonoBehaviour
{
    public AudioClip MorningClip;
    public AudioClip LunchClip;
    public AudioClip EveningClip;
    public AudioClip ButtonClickClip;
    private AudioSource BGMSource;
    private AudioSource ButtonClickSource;
    private Option Option;

    private void Start()
    {
        BGMSource = gameObject.AddComponent<AudioSource>();
        ButtonClickSource = gameObject.AddComponent<AudioSource>();


        Option = GameObject.Find("LobbyData").GetComponent<Option>();
        BGMSource.volume = Option.BGMVolume;
        ButtonClickSource.volume = Option.ClickVolume;

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
