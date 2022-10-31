using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask WhatIsGround, WhatIsPlayer;

    public Animator animator;

    private bool dead;
    public int maxHealth = 100;
    private int currentHealth;

    [SerializeField] private healthbar _healthbar;


    public int attackDamage = 20;


    //Patrolling
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttack;
    private bool alreadyAttacked;

    //states
    public float sightRange, AttackRange;
    public bool PlayerInSightRange, playerInAttackRange;

    private void Start()
    {
        currentHealth = maxHealth;
        dead = false;
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        _healthbar.UpdateHealthbar(maxHealth, currentHealth);

    }

    private void Update()
    {
        PlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);

        if (!dead && !PlayerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }
        if (!dead && PlayerInSightRange && !playerInAttackRange) 
        {
            ChasePlayer();
        }
        if (!dead && PlayerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }

    }

    private void Patrolling()
    {
        if (!walkPointSet) searchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            animator.SetBool("running", true);

        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void searchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y,transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 1f, WhatIsGround))
        {
            walkPointSet = true;
        }

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("running", true);
    }
    
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            animator.SetBool("running", false);

            Collider[] hitPlayer = Physics.OverlapSphere(player.position, AttackRange, WhatIsPlayer);

            foreach (Collider player in hitPlayer)
            {
                if (!player.GetComponent<PlayerCombat>().isDead())
                {
                    animator.SetTrigger("attack");
                    Debug.Log("hit enemy!");
                    player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
                }
                
               
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttack);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
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
            Invoke(nameof(DestroyEnemy), 6f);

        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,sightRange);
    }

}
