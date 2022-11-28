using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDismisser : MonoBehaviour
{
    public ModalCutsceneManager cutsceneManager;

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            cutsceneManager.DismissCutscene();
        }
    }
}
