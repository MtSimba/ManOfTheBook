using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemies;
    public LayerMask boss;



    private bool dead;
    public int maxHealth = 100;
    private int currentHealth;

    private bool alreadyAttacked;
    private bool blocking;
    public Interactable focus;

    [SerializeField] private healthbar _healthbar;

    float attackRange = 1f;
    public int attackDamage = 20;

    void Start()
    {
        alreadyAttacked = false;
        currentHealth = maxHealth;

        _healthbar.UpdateHealthbar(maxHealth, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButton(1))
        {
            animator.SetBool("blocking", true);
            blocking = true;
        }
        else
        {
            animator.SetBool("blocking", false);
            blocking = false;
        }



        if (!alreadyAttacked && Input.GetMouseButtonDown(0))
        {
            alreadyAttacked = true;
            Attack();

        }
        else
        {
            alreadyAttacked = false;
        }

    }


    void Attack()
    {
        animator.SetTrigger("attack");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, 1, enemies);
        Collider[] hitBoss = Physics.OverlapSphere(attackPoint.position, 2, boss);

        foreach (Collider boss in hitBoss)
        {
            Debug.Log("hit boss!");
            boss.GetComponent<bossController>().TakeDamage(attackDamage);
        }

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("hit enemy!");
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);

        }

   

    }

    public void TakeDamage(int damage)
    {
        if (!blocking)
        {
            currentHealth -= damage;
            _healthbar.UpdateHealthbar(maxHealth, currentHealth);


            animator.SetTrigger("hurt");

            if (currentHealth <= 0)
            {
                dead = true;
                animator.SetTrigger("death");
                Invoke(nameof(Death), 6f);

            }
        }
    }

    public bool isDead()
    {
        return dead == true ? true : false;
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
