using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem
{
    // Store the abilities that we can use
    private List<Ability> abilitiesList;

    // Create the different abilities
    public AbilitySystem()
    {
        abilitiesList = new List<Ability>();

        abilitiesList.Add(new Ability { spells = Spells.Fireball });
        abilitiesList.Add(new Ability { spells = Spells.Frostball });
        abilitiesList.Add(new Ability { spells = Spells.Earthball });
        abilitiesList.Add(new Ability { spells = Spells.Waterball });
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
            abilitiesList[0].activateAbility();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            abilitiesList[1].activateAbility();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            abilitiesList[2].activateAbility();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            abilitiesList[3].activateAbility();
        }
    }
}
