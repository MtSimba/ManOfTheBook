using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{

    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private float _reducespeed = 2;
    private Camera _cam;

    private float _target = 1;

    void Start()
    {
        _cam = Camera.main;
    }

    public void UpdateHealthbar(float MaxHealth, float CurrentHealth)
    {
        _target = CurrentHealth / MaxHealth;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
        _healthbarSprite.fillAmount =
            Mathf.MoveTowards(_healthbarSprite.fillAmount, _target, _reducespeed * Time.deltaTime);
    }
}
