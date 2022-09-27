using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public TMPro.TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    void Start()
    {
        populate();
    }

    public void SetVolume(float volume) {
        Debug.Log($"Set volume value to {volume}");
        audioMixer.SetFloat("masterVolume", volume);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetQuality(int index) {
        Debug.Log($"Set quality value to {index}");
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("qualityLevel", index);
    }

    public void SetFullScreen(bool value) {
        Debug.Log($"Set fullscreen value to {value}");
        Screen.fullScreen = value;
        PlayerPrefs.SetInt("fullScreen", value ? 1 : 0);
    }

    public void SetResolution(int index) {
        Debug.Log($"Set fullscreen value to {index}");
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolution", index);
    }

    private void populate() {
        qualityDropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
        fullscreenToggle.SetIsOnWithoutNotify(Screen.fullScreen);

        populateVolume();
        populateCurrentResolution();
    }

    private void populateVolume() {
        float volume;
        audioMixer.GetFloat("masterVolume", out volume);
        volumeSlider.SetValueWithoutNotify(volume);
    }

    private void populateCurrentResolution() {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for(int i = 0;i < resolutions.Length;i++) {
            int width = resolutions[i].width;
            int height = resolutions[i].height;
            
            string option = width + " x " + height;
            options.Add(option);

            int currentHeight = Screen.currentResolution.height;
            int currentWidth = Screen.currentResolution.width;

            if(width == currentWidth && height == currentHeight) {
                Debug.Log("Found a default screen resolution value");
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
}
