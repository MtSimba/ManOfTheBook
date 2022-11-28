using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainAbilitySystem : MonoBehaviour
{
    private AbilitySystem abilitySystem;
    private bool frostballAdded = false;
    public static mainAbilitySystem Instance { get; private set; }
    public Camera cam;
    public Transform firePoint;
    public Animator animator;

    [SerializeField] private UI_Ability_Bar uiAbilityBar;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.abilitySystem = new AbilitySystem(cam, firePoint, animator);
        this.uiAbilityBar.setAbilitySystem(abilitySystem);
    }

    // Update is called once per frame
    void Update()
    {
        // check if ability should be added
        if(PlayerPrefs.GetInt("Achievement-Fireball2") == 1 && frostballAdded == false)
        {
            abilitySystem.getAbilitiesList().Add(new Ability
            {
                spells = Spells.Frostball,
                projectileShooter = new ProjectileShooter(cam, GameObject.Find("vfx_water_projectile"), firePoint),
                sprite = Resources.Load<Sprite>("Sprites/fireball-sky-3"),
                spellCooldown = new SpellCooldown(2),
                keyCode = KeyCode.Alpha2
            });
            this.uiAbilityBar.update();
            frostballAdded = true;
        }

        // update system
        this.abilitySystem.Update();
    }
}
