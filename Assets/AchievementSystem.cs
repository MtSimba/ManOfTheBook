using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementSystem : MonoBehaviour
{
    //Stats to be monitored
    int jumps;
    int fireballs;

    // List of achievements
    public static List<Achievement> achievementList = new List<Achievement> 
    {
        new Achievement("Jump1", "Jump 1 time.", "Jump", 1),
        new Achievement("Jump3", "Jump 3 times.", "Jump", 3),
        new Achievement("Jump5", "Jump 5 times.", "Jump", 5),
        new Achievement("Jump10", "Jump 10 times.", "Jump", 10),
        new Achievement("Jump25", "Jump 25 times.", "Jump", 25),
        new Achievement("Fireball2", "Throw 2 fireballs. \nFrostball has been unlocked!", "Fireball", 2),
        new Achievement("Fireball5", "Throw 5 fireballs.", "Fireball", 5)
        // Add new achievements here :)
    };

    //Popup
    public GameObject popUp;
    public Image achievementIconHolder;
    public TMP_Text achievementDescription;

    

    // Start is called before the first frame update
    void Start()
    {
        //Resetting achievements - delete later when we save games
        PlayerPrefs.DeleteAll();

        //Get components
        achievementIconHolder.GetComponent<Image>();
        achievementDescription.GetComponent<TMP_Text>();
        
        // Setup initial achievement stats
        jumps = 0;
        fireballs = 0;

        // Subscribe to events
        PlayerMovement.PointOfInterest += POIReached;
        AbilitySystem.PointOfInterest += POIReached;
    }


    void OnDestroy()
    {
        // Unsubscribe to events
        PlayerMovement.PointOfInterest -= POIReached;
        AbilitySystem.PointOfInterest -= POIReached;
    }


    private void POIReached(string poi)
    {
        if(poi == "Jump"){
            jumps = jumps + 1;
            CheckAchievements(poi, jumps);
        } else if (poi == "Fireball"){
            fireballs = fireballs + 1;
            CheckAchievements(poi, fireballs);
        }
    }


    private void CheckAchievements(string poi, int count){
        string baseAchievementKey = "Achievement-";

        foreach (Achievement achievement in achievementList)
        {
            string achievementKey = baseAchievementKey + achievement.name;

            if (achievement.pointOfInterest == poi && PlayerPrefs.GetInt(achievementKey) != 1 && count >= achievement.countGoal)
            {
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

        SoundManager.PlaySound("Achievement");
        
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

    public Achievement(string Name, string Description, string POI, int CountGoal)
    {
        name = Name;
        description = Description;
        pointOfInterest = POI;
        countGoal = CountGoal;
    }
}
