using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static AudioClip backgroundMusic1;
    static AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic1 = Resources.Load<AudioClip>("backgroundMusic1");
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = backgroundMusic1;
        audioSource.Play(0);
    }


    public static void StopBackgroundMusic(){
        audioSource.Stop();
    }


    public static void StartBackgroundMusic(){
        audioSource.Play(0);
    }
}
