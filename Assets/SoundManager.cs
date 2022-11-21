using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip swordHitSound;
    public static AudioClip jumpSound;
    public static AudioClip achievementSound;
    static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Soundmanager start");
        swordHitSound = Resources.Load<AudioClip>("SwordHit");
        jumpSound = Resources.Load<AudioClip>("Jump");
        achievementSound = Resources.Load<AudioClip>("Achievement");

        audioSource = GetComponent<AudioSource>();
    }


    public static void PlaySound(string soundType){
        switch(soundType){
            case "Attack":
                audioSource.PlayOneShot(swordHitSound);
                break;
            case "Jump":
                audioSource.PlayOneShot(jumpSound);
                break;
            case "Achievement":
                audioSource.PlayOneShot(achievementSound);
                break;
        }
    }
}
