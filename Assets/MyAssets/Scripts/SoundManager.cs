using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip swordHitSound;
    public static AudioClip jumpSound;
    public static AudioClip achievementSound;
    public static AudioClip runningSound;
    static AudioSource audioSource;

    public static bool isSoundPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Soundmanager start");
        swordHitSound = Resources.Load<AudioClip>("SwordHit");
        jumpSound = Resources.Load<AudioClip>("Jump");
        achievementSound = Resources.Load<AudioClip>("Achievement");
        runningSound = Resources.Load<AudioClip>("Running");

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


    public static void PlaySoundContinuously(string soundType){
        switch(soundType){
            case "Running":
                if (!audioSource.isPlaying){
                    audioSource.clip = runningSound;
                    audioSource.Play(0);
                    isSoundPlaying = true;
                }
                break;
        }
    }

    
    public static void PlaySoundStop(){
        audioSource.Stop();
        isSoundPlaying = false;
    }
}
