using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldown
{
    public float cooldownTime = 3;
    public bool isOnCooldown { get; set; }
    private float temp;
    public Image imageCooldown { get; set; }

    public SpellCooldown(float cooldown)
    {
        cooldownTime = cooldown;
        isOnCooldown = false;
    }

    public void activateCooldown()
    {
        isOnCooldown = true;
        temp = 1;
        imageCooldown.fillAmount = 1.0f;
    }

    public void isAbilityOnCooldown()
    {
        if (isOnCooldown == true)
        {
            temp -= 1 / cooldownTime * Time.deltaTime;
            imageCooldown.fillAmount = temp;
            if (temp <= 0)
            {
                isOnCooldown = false;
            }
        }
    }
}
