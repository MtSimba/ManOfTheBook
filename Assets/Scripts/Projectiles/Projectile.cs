using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool collided;
    public LayerMask enemies;
    public LayerMask boss;
    public int attackDamage = 20;

    void OnCollisionEnter (Collision co)
    {
        if(co.gameObject.tag != "Bullet" && co.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            Collider[] hitEnemies = Physics.OverlapSphere(co.gameObject.transform.position, 10, enemies);
            Collider[] hitBoss = Physics.OverlapSphere(co.gameObject.transform.position, 10, boss);

            foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("hit enemy!");
                    enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);

            }

            foreach (Collider boss in hitBoss)
            {
                Debug.Log("hit boss!");
                boss.GetComponent<bossController>().TakeDamage(attackDamage);
            }
            Destroy(gameObject);
        }
    }
}
