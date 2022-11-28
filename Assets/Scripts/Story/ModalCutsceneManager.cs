using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalCutsceneManager : MonoBehaviour
{

    public GameObject mainGame;
    public CanvasGroup cutsceneGroup;
    public GameObject cutsceneObject;

    private bool fadeIn, fadeOut;
    private float fadeSpeed = 0.3F;

    void Start() {
        cutsceneGroup.alpha = 0;
        cutsceneObject.SetActive(false);
    }

    void Update() {
        if(fadeIn) {
            float fadeAmount = cutsceneGroup.alpha + (fadeSpeed*Time.deltaTime);
            
            cutsceneGroup.alpha = fadeAmount;

            if(cutsceneGroup.alpha >= 1) {
                fadeIn = false;
                mainGame.SetActive(false);
            }
        }

        if(fadeOut) {
            float fadeAmount = cutsceneGroup.alpha - (fadeSpeed*Time.deltaTime);

            cutsceneGroup.alpha = fadeAmount;

            if(cutsceneGroup.alpha <= 0) {
                fadeOut = false;
                cutsceneObject.SetActive(false);
            }
        }
    }

    public void StartCutscene() {
        cutsceneObject.SetActive(true);
        fadeOut = false;
        fadeIn = true;
    }

    public void DismissCutscene() {
        mainGame.SetActive(true);
        fadeIn = false;
        fadeOut = true;
    }
}

