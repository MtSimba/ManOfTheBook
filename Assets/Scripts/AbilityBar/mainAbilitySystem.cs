using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainAbilitySystem : MonoBehaviour
{
    private AbilitySystem abilitySystem;
    public static mainAbilitySystem Instance { get; private set; }
    public Camera cam;
    public Transform firePoint;

    [SerializeField] private UI_Ability_Bar uiAbilityBar;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.abilitySystem = new AbilitySystem(cam, firePoint);
        this.uiAbilityBar.setAbilitySystem(abilitySystem);
    }

    // Update is called once per frame
    void Update()
    {
        this.abilitySystem.Update();
    }
}
