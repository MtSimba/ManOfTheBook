using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    #region Singleton
    [SerializeField]
    public static PlayerMovement instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;
    public CharacterController controller;
    public Animator animator;
    public Transform cam;

    [SerializeField]
    public float _speed = 30f;
    [SerializeField]
    public float _rotationSpeed = 90f;
    [SerializeField]
    private float _gravity = -9.81f;
    [SerializeField]
    private float _jumpSpeed = 3.5f;

    private Vector3 moveVelocity;
    private Vector3 turnVelocity;

    private float targetAngle;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!instance.GetComponent<PlayerCombat>().isDead())
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");


            if (controller.isGrounded)
            {
                if (verticalInput > 0f)
                    animator.SetBool("running", true);
                else
                    animator.SetBool("running", false);

                moveVelocity = transform.forward * _speed * verticalInput;
                turnVelocity = transform.up * _rotationSpeed * horizontalInput;
                if (Input.GetButtonDown("Jump"))
                {
                    animator.SetTrigger("jumping");
                    moveVelocity.y = _jumpSpeed;
                }
            }

            moveVelocity.y += _gravity * Time.deltaTime;
            controller.Move(moveVelocity * Time.deltaTime);
            transform.Rotate(turnVelocity * Time.deltaTime);
        }
    }
}
