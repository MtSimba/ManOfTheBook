using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip swordHitSound;
    static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Soundmanager start");
        swordHitSound = Resources.Load<AudioClip>("SwordHit");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string soundType){
        switch(soundType){
            case "attack":
                audioSource.PlayOneShot(swordHitSound);
                break;
        }
    }
}
