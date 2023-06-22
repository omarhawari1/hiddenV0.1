using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class settings : MonoBehaviour
{
    [SerializeField]private player_main player_Main;
    [SerializeField]private GameObject pauseMenu;
    [SerializeField]private AudioMixer audioMixer;
    [SerializeField]private TMP_Dropdown resolutionDropdown;
    [SerializeField]private Slider audioSlider;
    [SerializeField] private Slider mouseSensSlider;

    private Resolution[] resolutions;

    private void Start()
    {
        float audioMixerValue;
        audioMixer.GetFloat("volume", out audioMixerValue);
        audioSlider.value = audioMixerValue;

        float mouseSensValue;
        mouseSensValue = player_Main.mouseSens;

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void back()
    {
        gameObject.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void setMouseSens(float sens)
    {
        player_Main.mouseSens = sens;
    }
    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void setResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
