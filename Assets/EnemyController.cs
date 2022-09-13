using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public GameObject Player;
    public NavMeshAgent agent;
    public Animator animator;
     // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(Player.transform.position);
            animator.SetBool("running", true);

            if (distance <= agent.stoppingDistance)
            {
                //attack
                FaceTarget();
            }
        }
        else
        {
            animator.SetBool("running", false);
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (Player.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.y));
        transform.rotation = Quaternion.Slerp(this.transform.rotation,lookRotation,Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }
}
