using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementMenu : MonoBehaviour
{
    private static bool gamePaused = false;
    public GameObject achievementsMenuUI;
    public GameObject pauseMenuUI;

    public TMP_Text jump_1_achievement;
    public TMP_Text jump_3_achievement;
    public TMP_Text jump_5_achievement;
    public TMP_Text jump_10_achievement;
    public TMP_Text jump_25_achievement;

    public Image checkmarkJump1;
    public Image checkmarkJump3;
    public Image checkmarkJump5;
    public Image checkmarkJump10;
    public Image checkmarkJump25;


    // Start is called before the first frame update
    void Start()
    {
        checkmarkJump1.GetComponent<Image>();
        checkmarkJump3.GetComponent<Image>();
        checkmarkJump5.GetComponent<Image>();
        checkmarkJump10.GetComponent<Image>();
        checkmarkJump25.GetComponent<Image>();

        //Setting achievement descriptions
        foreach (Achievement achievement in AchievementSystem.achievementList)
        {
            if (achievement.name == "JumpAchi1"){
                jump_1_achievement.text = achievement.description;
            }else if (achievement.name == "JumpAchi3"){
                jump_3_achievement.text = achievement.description;
            }else if (achievement.name == "JumpAchi5"){
                jump_5_achievement.text = achievement.description;
            }else if (achievement.name == "JumpAchi10"){
                jump_10_achievement.text = achievement.description;
            }else if (achievement.name == "JumpAchi25"){
                jump_25_achievement.text = achievement.description;
            }
        }

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
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            Resume();
        }
    }

    private void Resume() {
        if (!pauseMenuUI.activeSelf){
            achievementsMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gamePaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }

    private void Pause() {
        if (!pauseMenuUI.activeSelf){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            achievementsMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gamePaused = true;
        }
        


        //Refresh achievements
        checkmarkJump1.enabled = false;
        checkmarkJump3.enabled = false;
        checkmarkJump5.enabled = false;
        checkmarkJump10.enabled = false;
        checkmarkJump25.enabled = false;

        
        if (PlayerPrefs.GetInt("Achievement-JumpAchi1") == 1){
            checkmarkJump1.enabled = true;
        }
        if (PlayerPrefs.GetInt("Achievement-JumpAchi3") == 1){
            checkmarkJump3.enabled = true;
        }
        if (PlayerPrefs.GetInt("Achievement-JumpAchi5") == 1){
            checkmarkJump5.enabled = true;
        }
        if (PlayerPrefs.GetInt("Achievement-JumpAchi10") == 1){
            checkmarkJump10.enabled = true;
        }
        if (PlayerPrefs.GetInt("Achievement-JumpAchi25") == 1){
            checkmarkJump25.enabled = true;
        }
        
    }
}
