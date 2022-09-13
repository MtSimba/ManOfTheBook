using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vertical", Input.GetAxis("vertical"));
        anim.SetFloat("horizontal", Input.GetAxis("horizontal"));

    }
}
