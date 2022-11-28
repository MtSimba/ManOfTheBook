using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerScript : MonoBehaviour
{
    public Animator animator;

    public void GoNextScene() {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
