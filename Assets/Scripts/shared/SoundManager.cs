using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip swordHitSound;
    public static AudioClip swordMissSound;
    public static AudioClip jumpSound;
    public static AudioClip achievementSound;
    public static AudioClip runningSound;
    public static AudioClip fireballSound;
    public static AudioClip frostballSound;
    static AudioSource audioSource;

    public static bool isSoundPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        swordHitSound = Resources.Load<AudioClip>("SwordHit");
        swordMissSound = Resources.Load<AudioClip>("SwordMiss");
        jumpSound = Resources.Load<AudioClip>("Jump");
        achievementSound = Resources.Load<AudioClip>("Achievement");
        runningSound = Resources.Load<AudioClip>("Running");
        fireballSound = Resources.Load<AudioClip>("Fireball");
        frostballSound = Resources.Load<AudioClip>("Frostball");

        audioSource = GetComponent<AudioSource>();
    }


    public static void PlaySound(string soundType){
        switch(soundType){
            case "AttackHit":
                audioSource.PlayOneShot(swordHitSound);
                break;
            case "AttackMiss":
                audioSource.PlayOneShot(swordMissSound);
                break;
            case "Jump":
                audioSource.PlayOneShot(jumpSound);
                break;
            case "Achievement":
                audioSource.PlayOneShot(achievementSound);
                break;
            case "Fireball":
                audioSource.PlayOneShot(fireballSound);
                break;
            case "Frostball":
                audioSource.PlayOneShot(frostballSound);
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
