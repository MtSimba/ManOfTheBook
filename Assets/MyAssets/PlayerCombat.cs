using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    
    public Transform attackPoint;
    public LayerMask enemies;

    private bool dead;
    public int maxHealth = 100;
    private int currentHealth;

    [SerializeField] private healthbar _healthbar;

    float attackRange = 1f;
    public int attackDamage = 20;

    void Start()
    {
        currentHealth = maxHealth;

        _healthbar.UpdateHealthbar(maxHealth,currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("attack");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemies);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("hit enemy!");
            enemy.GetComponent<EnemyBehaviour>().TakeDamage(attackDamage);
        }

    }

    public void TakeDamage(int damage)
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
