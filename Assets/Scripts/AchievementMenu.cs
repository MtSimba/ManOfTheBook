using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject achievementsMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back(){
        pauseMenuUI.SetActive(true);
        achievementsMenuUI.SetActive(false);
    }
}
