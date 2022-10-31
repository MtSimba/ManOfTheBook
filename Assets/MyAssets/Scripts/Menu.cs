using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private Resolution[] resolutions;

    void Start() {
        loadPlayerPrefs();
    }

    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit() {
        Application.Quit();
        Debug.Log("Quit pressed");
    }

    private void loadPlayerPrefs() {
        // Volume
        audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("masterVolume", -20.0f));
        // Quality
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel", 5));
        // Fullscreen
        Screen.fullScreen = PlayerPrefs.GetInt("fullScreen", 1) == 1 ? true : false;
        
        // Resolution
        int res = PlayerPrefs.GetInt("resolution", 0);
        resolutions = Screen.resolutions;
        var preferredRes = resolutions[res];
        Screen.SetResolution(preferredRes.width, preferredRes.height, Screen.fullScreen);
    }
}
