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
    public Camera cam;
    private Interactable focus;
    [SerializeField] private Transform interactionTransform;  // The transform from where we interact in case you want to offset it
    [SerializeField] private LayerMask mask;
    [SerializeField] private float radius = 0.5f;               // How close do we need to be to interact?

    private readonly Collider[] colliders = new Collider[3];


    [SerializeField]
    public float _speed = 6f;
    [SerializeField]
    public float _rotationSpeed = 90f;
    [SerializeField]
    private float _gravity = -9.81f;
    [SerializeField]
    private float _jumpSpeed = 3.5f;

    private Vector3 moveVelocity;
    private Vector3 turnVelocity;

    private int numFound;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!instance.GetComponent<PlayerCombat>().isDead())
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            numFound = Physics.OverlapSphereNonAlloc(interactionTransform.position, radius, colliders, mask);


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

                if (numFound > 0 && Input.GetKeyDown(KeyCode.E))
                {
                    var interactable = colliders[0].GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                }

                moveVelocity.y += _gravity * Time.deltaTime;
                controller.Move(moveVelocity * Time.deltaTime);
                transform.Rotate(turnVelocity * Time.deltaTime);
            }

            moveVelocity.y += _gravity * Time.deltaTime;
            controller.Move(moveVelocity * Time.deltaTime);
            transform.Rotate(turnVelocity * Time.deltaTime);
        }
    }

    // Set our focus to a new focus
    void SetFocus(Interactable newFocus)
    {
        // If our focus has changed
        if (newFocus != focus)
        {
            // Defocus the old one
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;   // Set our new focus
        }

        newFocus.OnFocused(transform);
    }

    // Remove our current focus
    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
    }
}
