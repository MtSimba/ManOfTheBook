using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    public NavMeshAgent agent;
    public Animator animator;

    public LayerMask playerMask;

    private bool dead;
    private int currentHealth;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private healthbar _healthbar;
    [SerializeField] public int attackDamage = 20;

    //Attacking
    public float timeBetweenAttack;
    private bool alreadyAttacked;

    //states
    public float AttackRange;
    public bool playerInAttackRange;
    public float lookRadius;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        dead = false;
        target = PlayerMovement.instance.interactionTransform.transform;
        agent = GetComponent<NavMeshAgent>();
        _healthbar.UpdateHealthbar(maxHealth, currentHealth);

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, playerMask);


        if (!dead && distance <= lookRadius)
        {
            animator.SetBool("running", true);
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                animator.SetBool("running", false);
                FaceTarget();          
                AttackPlayer();
            }
        }
        else
        {
            animator.SetBool("running", false);
        }

    }

    private void AttackPlayer()
    {

        if (!alreadyAttacked)
        {

            Collider[] hitPlayer = Physics.OverlapSphere(target.position, AttackRange, playerMask);

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
            Invoke(nameof(ResetAttack), timeBetweenAttack);
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

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);

    }
}
