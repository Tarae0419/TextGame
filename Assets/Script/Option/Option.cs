using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public float BGMVolume;
    public float ClickVolume;
    public int FontSize;
    private int currentResolutionIndex;
    private int selectedResolutionIndex;
    private AudioSource audioSource;
    private List<string> FontOptions;
    public TMP_Dropdown fontSizeDropdown;
    public TextMeshProUGUI FontLabel;
    public TMP_Dropdown ResolutionDropdown;
    public Scrollbar BGMScrollbar;
    public Scrollbar SoundEeffectScrollbar;
    public Button ApplyButton;
    public TextMeshProUGUI TestText;
    private ResolutionData ResolutionData;
    public BGMManager BGMManager;
    public GameManager GameManager;

    void Start()
    {
        ResolutionData = new ResolutionData();

        BGMManager.BGMSource.volume = BGMScrollbar.value;
        BGMManager.ButtonClickSource.volume = SoundEeffectScrollbar.value;

        SetOptionDropdown();
        SetResolutionDropdown();

        SetVolume();
        SetFont();

        BGMScrollbar.onValueChanged.AddListener(delegate { ChangeBGMVolume(); });
        SoundEeffectScrollbar.onValueChanged.AddListener(delegate { ChangeClickVolume(); });
        fontSizeDropdown.onValueChanged.AddListener(delegate { ChangeFontSize(); });
        ResolutionDropdown.onValueChanged.AddListener(delegate { UpdateResolutionIndex(); });
        ApplyButton.onClick.AddListener(delegate { ApplyResolution(); });
    }

    public void SetVolume()
    {
        BGMManager.BGMSource.volume = PlayerPrefs.GetFloat("BGMVolume");
        BGMManager.ButtonClickSource.volume = PlayerPrefs.GetFloat("ClickVolume");
        BGMScrollbar.value = PlayerPrefs.GetFloat("BGMVolume");
        SoundEeffectScrollbar.value = PlayerPrefs.GetFloat("ClickVolume");
    }

    public void SetFont()
    {
        var FontSize = PlayerPrefs.GetInt("FontSize");
        if (FontSize != 17 || FontSize != 18 || FontSize != 19 || FontSize != 20 || FontSize != 21 || FontSize != 22)
            FontSize = 17;
        PlayerPrefs.SetInt("FontSize", FontSize);
        TestText.fontSize = FontSize;


        for (int i = 0; i < fontSizeDropdown.options.Count; i++)
        {
            string optionText = fontSizeDropdown.options[i].text;

            if (int.TryParse(optionText, out int fontSize) && fontSize == FontSize)
            {
                fontSizeDropdown.value = i;
                break;
            }
        }
    }

    public void ChangeBGMVolume()
    {
        PlayerPrefs.SetFloat("BGMVolume", BGMScrollbar.value);
        BGMManager.BGMSource.volume = PlayerPrefs.GetFloat("BGMVolume");
    }

    public void ChangeClickVolume()
    {
        PlayerPrefs.SetFloat("ClickVolume", SoundEeffectScrollbar.value);
        BGMManager.ButtonClickSource.volume = PlayerPrefs.GetFloat("ClickVolume");
    }

    public void ChangeFontSize()
    {
        string FontSizeString = fontSizeDropdown.options[fontSizeDropdown.value].text;
        int FontSize = int.Parse(FontSizeString);


        PlayerPrefs.SetInt("FontSize", FontSize);
        TestText.fontSize = PlayerPrefs.GetInt("FontSize");
        if (SceneManager.GetActiveScene().name == "TextScene")
            GameManager.SetFont();
    }

    public void SetOptionDropdown()
    {
        FontOptions = new List<string> { "17", "18", "19", "20", "21", "22"};
        fontSizeDropdown.ClearOptions();
        fontSizeDropdown.AddOptions(FontOptions);
    }

    public void SetResolutionDropdown()
    {
        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        currentResolutionIndex = 0;

        for (int i = 0; i < ResolutionData.width.Count; i++)
        {
            string Resolutionoption = ResolutionData.width[i] + " x " + ResolutionData.height[i];
            options.Add(Resolutionoption);

            if (ResolutionData.width[i] == Screen.currentResolution.width &&
                ResolutionData.height[i] == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentResolutionIndex;
    }

    public void UpdateResolutionIndex()
    {
        selectedResolutionIndex = ResolutionDropdown.value;
    }

    public void ApplyResolution()
    {
        Screen.SetResolution(ResolutionData.width[selectedResolutionIndex], ResolutionData.height[selectedResolutionIndex], Screen.fullScreen);
    }

}
