using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem
{
    // Store the abilities that we can use
    private List<Ability> abilitiesList;
    private float cooldown;
    private float temp;
    private bool isOnCooldown;
    private Animator Myanimator;
    // Create the different abilities
    public AbilitySystem(Camera cam, Transform firePoint, Animator animator)
    {
        abilitiesList = new List<Ability>();
        Myanimator = animator;

        abilitiesList.Add(new Ability
        {
            spells = Spells.Fireball,
            projectileShooter = new ProjectileShooter(cam, GameObject.Find("vfx_fire_projectile"), firePoint),
            sprite = Resources.Load<Sprite>("Sprites/fireball-red-3")
    });
        abilitiesList.Add(new Ability
        {
            spells = Spells.Frostball,
            projectileShooter = new ProjectileShooter(cam, GameObject.Find("vfx_water_projectile"), firePoint),
            sprite = Resources.Load<Sprite>("Sprites/fireball-sky-3")
        });
        abilitiesList.Add(new Ability
        {
            spells = Spells.Earthball,
            projectileShooter = new ProjectileShooter(cam, GameObject.Find("vfx_fire_projectile"), firePoint),
            sprite = Resources.Load<Sprite>("Sprites/protect-acid-2")
        });
        abilitiesList.Add(new Ability
        {
            spells = Spells.Waterball,
            projectileShooter = new ProjectileShooter(cam, GameObject.Find("vfx_water_projectile"), firePoint),
            sprite = Resources.Load<Sprite>("Sprites/evil-eye-eerie-3")
        });

        cooldown = 5;
        temp = 1;
        isOnCooldown = false;
    }

    public List<Ability> getAbilitiesList()
    {
        return this.abilitiesList;
    }

    private void activateCooldown()
    {
        isOnCooldown = true;
        temp = 1;
        Debug.Log("Ability shot");
    }

    // Update is called once per frame
    public void Update()
    {
        if (isOnCooldown == true)
        {
            Debug.Log("Now on cooldown");
            temp -= 1 / cooldown * Time.deltaTime;

            if (temp <= 0)
            {
                Debug.Log("Cooldown over");
                isOnCooldown = false;
                temp = 0;
            }
        }
        else if (isOnCooldown == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Myanimator.SetTrigger("spell_cast");
                abilitiesList[0].projectileShooter.ShootProjectile();
                activateCooldown();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Myanimator.SetTrigger("spell_cast");
                abilitiesList[1].projectileShooter.ShootProjectile();
                activateCooldown();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Myanimator.SetTrigger("spell_cast");
                abilitiesList[2].projectileShooter.ShootProjectile();
                activateCooldown();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Myanimator.SetTrigger("spell_cast");
                abilitiesList[3].projectileShooter.ShootProjectile();
                activateCooldown();
            }
        }
    }
}