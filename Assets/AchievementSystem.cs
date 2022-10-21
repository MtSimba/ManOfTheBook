using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    //Stats to be monitored
    int jumps;


    // List of achievements
    List<int> validJumpCounts = new List<int> { 1, 3, 5, 10, 20 };


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();

        // Setup initial achievement stats
        jumps = 0;

        move.PointOfInterest += POIReached;
    }


    // Update is called once per frame
    void Update()
    {

    }


    private void POIReached(string poi)
    {
        if(poi == "Jump"){
            jumps = jumps + 1;
            CheckAchievements(poi, jumps, validJumpCounts);
        }
    }


    private void CheckAchievements(string poi, int count, List<int> validCounts){
        string baseAchievementKey = "Achievement-" + poi + "-";

        if (validCounts.Contains(count)){
            string achievementKey = baseAchievementKey + count.ToString();

                if (PlayerPrefs.GetInt(achievementKey) == 1){
                    return;
                }

                PlayerPrefs.SetInt(achievementKey, 1);
                Debug.Log("Unlocked " + achievementKey);
        }
    }
}
