using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cam;
    [Header("Movement")]
    public Transform orientation;
    public Animator animator;
    public float groundDrag;
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    private bool readyToJump;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float PlayerHeight;
    public LayerMask WhatisGround;
    private bool grounded;
    /*#region Singleton

    public static PlayerMovement instance;

    #endregion*/

    public float rotationSpeed = 0.25f;
    public float moveSpeed = 6f;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    void Start()
    {
        animator.SetBool("running", false);
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, WhatisGround);

        playerInput();
        rotation();
        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void playerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        { 
            animator.SetTrigger("jumping");
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump),jumpCoolDown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = new Vector3(horizontalInput,0f,verticalInput);
        moveDirection = Quaternion.AngleAxis(cam.rotation.eulerAngles.y, Vector3.up) * moveDirection;
        moveDirection.Normalize();
        if (moveDirection.magnitude >= 0.1f)
        {
            transform.forward = moveDirection;
            
            animator.SetBool("running", true);
        }
        else
            animator.SetBool("running", false);


        if (grounded)
        {
            transform.Translate(moveDirection * moveSpeed  * Time.deltaTime, Space.World);
            //rb.AddForce(moveDirection * moveSpeed * 10f,ForceMode.Force);
        }
        else if (!grounded)
        {
            transform.Translate(moveDirection * moveSpeed  * Time.deltaTime * airMultiplier, Space.World);
            //rb.AddForce(moveDirection * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

    }

    private void rotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = moveDirection.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = moveDirection.z;

        Quaternion currentRotation = transform.rotation;

        if (moveDirection.magnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
       
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x,0f,rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limited = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limited.x,rb.velocity.y,limited.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce,ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

}
