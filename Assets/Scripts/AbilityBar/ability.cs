using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ability
{
    public Spells spells;
    public ProjectileShooter projectileShooter;
    public Sprite sprite;
    public float cooldownTime = 3;
    public bool isOnCooldown { get; set; }
    public KeyCode keyCode;
    private float temp;

    public void activateCooldown()
    {
        isOnCooldown = true;
        temp = 1;
    }

    public void isAbilityOnCooldown()
    {
        if (isOnCooldown == true)
        {
            temp -= 1 / cooldownTime * Time.deltaTime;
            if (temp <= 0)
            {
                isOnCooldown = false;
            }
        }
    }
}


