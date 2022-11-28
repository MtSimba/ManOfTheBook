using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ability
{
    public Spells spells;
    public ProjectileShooter projectileShooter;
    public Sprite sprite;
    public SpellCooldown spellCooldown { get; set; }
    public KeyCode keyCode;

    public void activateCooldown()
    {
        this.spellCooldown.activateCooldown();
    }

    public void isAbilityOnCooldown()
    {
        this.spellCooldown.isAbilityOnCooldown();
    }

    public bool isOnCooldown()
    {
        return this.spellCooldown.isOnCooldown;
    }
}


