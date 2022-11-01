using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStory : MonoBehaviour
{
    void OnEnable()
    {
        Debug.Log("Enabled Introstory loading");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
