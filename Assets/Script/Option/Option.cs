using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    public TMP_Dropdown ResolutionDropdown;
    public Scrollbar BGMScrollbar;
    public Scrollbar SoundEeffectScrollbar;
    public Button ApplyButton;
    public TextMeshProUGUI TestText;
    private ResolutionData ResolutionData;
    public BGMManager BGMManager;
    Resolution[] resolutions;

    void Start()
    {
        ResolutionData = new ResolutionData();

        BGMManager.BGMSource.volume = BGMScrollbar.value;
        BGMManager.ButtonClickSource.volume = SoundEeffectScrollbar.value;

        SetOptionDropdown();
        SetResolutionDropdown();


        BGMScrollbar.onValueChanged.AddListener(delegate { ChangeBGMVolume(); });
        SoundEeffectScrollbar.onValueChanged.AddListener(delegate { ChangeClickVolume(); });
        fontSizeDropdown.onValueChanged.AddListener(delegate { ChangeFontSize(); });
        ResolutionDropdown.onValueChanged.AddListener(delegate { UpdateResolutionIndex(); });
        ApplyButton.onClick.AddListener(delegate { ApplyResolution(); });
    }

    public void ChangeBGMVolume()
    {
        PlayerPrefs.SetFloat("BGMVolume", BGMScrollbar.value);
        BGMManager.BGMSource.volume = BGMScrollbar.value;
    }

    public void ChangeClickVolume()
    {
        PlayerPrefs.SetFloat("ClickVolume", SoundEeffectScrollbar.value);
        BGMManager.ButtonClickSource.volume = SoundEeffectScrollbar.value;
    }

    public void ChangeFontSize()
    {
        PlayerPrefs.SetInt("FontSize", int.Parse(fontSizeDropdown.options[fontSizeDropdown.value].text));
        TestText.fontSize = int.Parse(fontSizeDropdown.options[fontSizeDropdown.value].text);
    }

    public void SetOptionDropdown()
    {
        FontOptions = new List<string> { "17", "18", "19", "20", "21", "22", "23"};
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
