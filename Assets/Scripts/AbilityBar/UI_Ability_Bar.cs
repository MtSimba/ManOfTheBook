using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Ability_Bar : MonoBehaviour
{
    private Transform abilitySlotTemplate;
    private AbilitySystem abilitySystem;
    
    private void Awake()
    {
        abilitySlotTemplate = transform.Find("abilitySlotTemplate");
        abilitySlotTemplate.gameObject.SetActive(false);
    }

    public void setAbilitySystem(AbilitySystem abilitySystem)
    {
        this.abilitySystem = abilitySystem;
        updateVisual();
    }

    private void updateVisual()
    {
        List<Ability> abilitiesList = abilitySystem.getAbilitiesList();
        Debug.LogWarning(abilitiesList.Count);
        for (int i = 0; i < abilitiesList.Count; i++)
        {
            Ability ability = abilitiesList[i];
            Transform abilitySlotTransform = Instantiate(this.abilitySlotTemplate, transform);
            abilitySlotTransform.gameObject.SetActive(true);
            RectTransform abilitySlotRectTransform = abilitySlotTransform.GetComponent<RectTransform>();
            abilitySlotRectTransform.anchoredPosition = new Vector2(120f * i, 50f);
            abilitySlotTransform.Find("number").GetComponent<TMPro.TextMeshProUGUI>().SetText((i + 1).ToString());
        }

    }
}
