using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class move : MonoBehaviour
{
    [SerializeField]
    public CharacterController controller;
    [SerializeField]
    public Animator animator;
    [SerializeField]
    private Transform camera;
    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    private float gravity = 9.81f;
    [SerializeField]
    private float _jumpSpeed = 3.5f;

    private float directionY;
    private float targetAngle;

    //public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", horizontalInput);
        animator.SetFloat("Vertical", verticalInput);

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        direction = Quaternion.AngleAxis(camera.rotation.eulerAngles.y, Vector3.up) * direction;

        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Jumping");
                directionY = _jumpSpeed;
            }
        }

        if (direction.magnitude >= 0.1f)
        {
            if (verticalInput > 0)
            {
                targetAngle = MathF.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            }
            directionY -= gravity * Time.deltaTime;

            direction.y = directionY;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}

/*
 *  float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _rotationSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
 */


/*
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
*/

/*
 
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
*/