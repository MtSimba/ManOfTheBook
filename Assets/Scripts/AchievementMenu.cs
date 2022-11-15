using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject achievementsMenuUI;

    public TMP_Text jump_1_achievement; //TODO
    public TMP_Text jump_3_achievement;
    public TMP_Text jump_5_achievement;
    public TMP_Text jump_10_achievement;
    public TMP_Text jump_25_achievement;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y)) {
            if(gamePaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        achievementsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        achievementsMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;


        //Refresh achievements
        //TODO Ugliest code ever written, fix
        string achievementText = "";

        foreach (Achievement achievement in AchievementSystem.achievementList)
        {
            if (achievement.name == "JumpAchi1"){
                achievementText = achievement.description;
                if (PlayerPrefs.GetInt("Achievement-" + achievement.name) == 1){
                    achievementText = achievementText + " DONE";
                }
                jump_1_achievement.text = achievementText;
            }else if (achievement.name == "JumpAchi3"){
                achievementText = achievement.description;
                if (PlayerPrefs.GetInt("Achievement-" + achievement.name) == 1){
                    achievementText = achievementText + " DONE";
                }
                jump_3_achievement.text = achievementText;
            }else if (achievement.name == "JumpAchi5"){
                achievementText = achievement.description;
                if (PlayerPrefs.GetInt("Achievement-" + achievement.name) == 1){
                    achievementText = achievementText + " DONE";
                }
                jump_5_achievement.text = achievementText;
            }else if (achievement.name == "JumpAchi10"){
                achievementText = achievement.description;
                if (PlayerPrefs.GetInt("Achievement-" + achievement.name) == 1){
                    achievementText = achievementText + " DONE";
                }
                jump_10_achievement.text = achievementText;
            }else if (achievement.name == "JumpAchi25"){
                achievementText = achievement.description;
                if (PlayerPrefs.GetInt("Achievement-" + achievement.name) == 1){
                    achievementText = achievementText + " DONE";
                }
                jump_25_achievement.text = achievementText;
            }
        }
    }
}
