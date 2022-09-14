using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    #region Singleton

    public static PlayerMovement instance;

    #endregion

    public GameObject player;

    public float speed = 6f;

    private float turnSmoothVelocity;

    public float turnSmoothTIme = 0.1f;

    void Awake()
    {
        instance = this;
    }

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
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(direction * speed * Time.deltaTime);

        }
        else
        {
            animator.SetBool("running", false);
        }
    }
}
