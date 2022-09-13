using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdpersonscript : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public float speed = 6f;

    private float turnSmoothVelocity;

    public float turnSmoothTIme = 0.1f;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        animator.SetFloat("Horizontal", horizontal);
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {   
            animator.SetBool("running", true);
            float targetAngle = MathF.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTIme);
            transform.rotation = Quaternion.Euler(0f,angle,0f);
            controller.Move(direction * speed * Time.deltaTime);

        }
        else
        {
            animator.SetBool("running", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            attack();
        }
    }

    void attack()
    {
       animator.SetTrigger("attack");
       
       Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

       foreach (Collider enemy in hitEnemies)
       {
           Debug.Log("Hit!" + enemy.name);
       }

    }

    void OnDrawGizmosSelcted()
    {
        if (!attackPoint)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }

}
