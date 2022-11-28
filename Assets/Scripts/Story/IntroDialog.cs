using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroDialog : MonoBehaviour
{

    public SceneChangerScript levelChanger;
    public TMPro.TMP_Text dialogText;

    private int counter = 0;

    private string[] dialogs = new string[] { 
        "Finally, you're awake",
        "Welcome stranger",
        "You have arrived at the Library of Babel",
        "In this library you will find books containing every possible ordering of the letters a to z",
        "What that means is that you can find every book ever written",
        "But you can also find every book that will ever be written. This place is an infinite source of knowledge",
        "We, the Librarians, have been expecting your arrival", 
        "We are in dire need of assistance", 
        "For long, we have been living in peace, obtaining knowledge and educating our people",
        "To us, every book in the library has its place",
        "But not long ago, a certain group of people known as the purists began burning books that they find unworthy",
        "A war is breaking out, and we don't know how to stop it",
        "You must seek out the mythic Man of The Book. He is said to be the only person who has read the index file", 
        "The index file contains an overview of every book in the library", 
        "Only The Man of The Book has the knowledge to end this war once and for all",
        "He is our only hope",
        "Good luck stranger, you are gonna need it" 
    };

    void Start()
    {
        dialogText.text = dialogs[counter] + " ...";
        levelChanger = GameObject.FindGameObjectWithTag("LevelChanger").GetComponent<SceneChangerScript>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            skipDialog();
        }
    }

    private void skipDialog() {
        counter++;

        if(counter >= dialogs.Length) {
            levelChanger.GoNextScene();
        } else {
            dialogText.text = dialogs[counter] + " ...";
        }
    }
}
