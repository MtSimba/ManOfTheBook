using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilitySystem
{
    // Store the abilities that we can use
    private List<Ability> abilitiesList { get; set; }

    //Achievement
    public static event Action<string> PointOfInterest;

    // Create the different abilities
    public AbilitySystem(Camera cam, Transform firePoint)
    {
        abilitiesList = new List<Ability>();

        abilitiesList.Add(new Ability
        {
            spells = Spells.Fireball,
            projectileShooter = new ProjectileShooter(cam, GameObject.Find("vfx_fire_projectile"), firePoint),
            sprite = Resources.Load<Sprite>("Sprites/fireball-red-3"),
            isOnCooldown = false,
            cooldownTime = 4,
            keyCode = KeyCode.Alpha1
        });
    }

    public List<Ability> getAbilitiesList()
    {
        return this.abilitiesList;
    }

    // Update is called once per frame
    public void Update()
    {
        foreach (Ability ability in abilitiesList)
        {
            if (Input.GetKeyDown(ability.keyCode))
            {
                if (!ability.isOnCooldown)
                {
                    ability.projectileShooter.ShootProjectile();
                    SoundManager.PlaySound(ability.spells.ToString());
                    ability.activateCooldown();
                    //Achievement
                    PointOfInterest(ability.spells.ToString());
                }
            }
            ability.isAbilityOnCooldown();
        }
    }
}