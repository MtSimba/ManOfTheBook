using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class move : MonoBehaviour
{

    public CharacterController controller;
    public Animator animator;
    public Transform cam;
    public Transform player;

    [SerializeField]
    public float _speed = 30f;
    [SerializeField]
    public float _rotationSpeed = 90f;
    float _turnSmoothVelocity;
    [SerializeField]
    private float _gravity = -9.81f;
    [SerializeField]
    private float _jumpSpeed = 3.5f;

    private Vector3 moveVelocity;
    private Vector3 turnVelocity;

    private float targetAngle;

    //Achievement
    public static event Action<string> PointOfInterest;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<PlayerCombat>().isDead())
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
                    //Achievement
                    PointOfInterest("Jump");

                    SoundManager.PlaySound("Jump");

                    animator.SetTrigger("jumping");
                    moveVelocity.y = _jumpSpeed;
                }
            }
            //targetAngle = Mathf.Atan2(moveVelocity.x, moveVelocity.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _rotationSpeed);

            moveVelocity.y += _gravity * Time.deltaTime;
            controller.Move(moveVelocity * Time.deltaTime);
            transform.Rotate(turnVelocity * Time.deltaTime);
        }
    }
}

/*
 *  float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _rotationSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
 */
