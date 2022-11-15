using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//TODO: Bug when starting from Main Menu scene, where AchievementSystem is not initialized mby? Jump achi doesnt work

public class AchievementSystem : MonoBehaviour
{
    //Stats to be monitored
    int jumps;

    // List of achievements
    // TODO: Use these for Achievement Menu
    public static List<Achievement> achievementList = new List<Achievement> 
    {
        new Achievement("JumpAchi1", "Jump 1 time.", "Jump", 1),
        new Achievement("JumpAchi3", "Jump 3 times.", "Jump", 3),
        new Achievement("JumpAchi5", "Jump 5 times.", "Jump", 5),
        new Achievement("JumpAchi10", "Jump 10 times.", "Jump", 10),
        new Achievement("JumpAchi25", "Jump 25 times.", "Jump", 25)
    };

    //Popup
    public GameObject popUp;
    public Image achievementIconHolder;
    public TMP_Text achievementDescription;
    

    // Start is called before the first frame update
    void Start()
    {
        //Resetting achievements - delete layer when we save games
        PlayerPrefs.DeleteAll();

        //Get components
        achievementIconHolder.GetComponent<Image>();

        // Setup initial achievement stats
        jumps = 0;

        // Subscribe to events
        move.PointOfInterest += POIReached;

        // We need to make sure that all achievements are saved, also when the game is quit and reopened!
    }


    // Update is called once per frame
    void Update()
    {

    }


    private void POIReached(string poi)
    {
        if(poi == "Jump"){
            jumps = jumps + 1;
            CheckAchievements(poi, jumps);
        }
    }


    private void CheckAchievements(string poi, int count){
        string baseAchievementKey = "Achievement-";

        foreach (Achievement achievement in achievementList)
        {
            if (achievement.pointOfInterest == poi && achievement.achievementReached != true && count >= achievement.countGoal)
            {
                achievement.achievementReached = true;

                string achievementKey = baseAchievementKey + achievement.name;
                PlayerPrefs.SetInt(achievementKey, 1);

                // Debug.Log("Unlocked " + achievementKey);

                Sprite sprite = Resources.Load<Sprite>("Sprites/" + achievement.name);
                achievementIconHolder.sprite = sprite;
                achievementDescription.text = achievement.description;

                StartCoroutine(ShowPopup());
            }
        }
    }


    IEnumerator ShowPopup()
    {
        popUp.SetActive(true);

        //TODO: Play achievement sound
        
        yield return new WaitForSeconds(3);

        popUp.SetActive(false);
    }
}


public class Achievement
{
    public string name;
    public string description;
    public string pointOfInterest;
    public int countGoal;
    public bool achievementReached;

    public Achievement(string Name, string Description, string POI, int CountGoal)
    {
        name = Name;
        description = Description;
        pointOfInterest = POI;
        countGoal = CountGoal;
        achievementReached = false;
    }
}
