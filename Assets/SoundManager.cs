using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip swordHitSound;
    public static AudioClip jumpSound;
    static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Soundmanager start");
        swordHitSound = Resources.Load<AudioClip>("SwordHit");
        jumpSound = Resources.Load<AudioClip>("Jump");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string soundType){
        switch(soundType){
            case "Attack":
                audioSource.PlayOneShot(swordHitSound);
                break;
            case "Jump":
                audioSource.PlayOneShot(jumpSound);
                break;
        }
    }
}
