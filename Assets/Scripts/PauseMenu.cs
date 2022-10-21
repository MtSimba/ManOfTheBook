using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject achievementsMenuUI;

    void Start() {
        Resume();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(gamePaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void DidTapMainMenu() {
        Debug.Log("Did exit to main menu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Pause() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Achievements(){
        Debug.Log("Pressed Achievements");
        achievementsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void Settings(){
        Debug.Log("Pressed Settings");
    }

    public void Quit(){
        Debug.Log("Pressed Quit");
    }
}
