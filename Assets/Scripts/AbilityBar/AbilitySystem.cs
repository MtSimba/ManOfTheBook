using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem
{
    // Store the abilities that we can use
    private List<Ability> abilitiesList;

    // Create the different abilities
    public AbilitySystem(Camera cam, Transform firePoint)
    {
        abilitiesList = new List<Ability>();

        abilitiesList.Add(new Ability { spells = Spells.Fireball, projectileShooter = new ProjectileShooter(cam, GameObject.Find("vfx_fire_projectile"), firePoint ) });
        abilitiesList.Add(new Ability { spells = Spells.Frostball, projectileShooter = new ProjectileShooter(cam, GameObject.Find("vfx_water_projectile"), firePoint) });
        abilitiesList.Add(new Ability { spells = Spells.Earthball, projectileShooter = new ProjectileShooter(cam, GameObject.Find("vfx_fire_projectile"), firePoint) });
        abilitiesList.Add(new Ability { spells = Spells.Waterball, projectileShooter = new ProjectileShooter(cam, GameObject.Find("vfx_water_projectile"), firePoint) });
    }

    public List<Ability> getAbilitiesList()
    {
        return this.abilitiesList;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            abilitiesList[0].projectileShooter.ShootProjectile();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            abilitiesList[1].projectileShooter.ShootProjectile();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            abilitiesList[2].projectileShooter.ShootProjectile();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            abilitiesList[3].projectileShooter.ShootProjectile();
        }
    }
}
