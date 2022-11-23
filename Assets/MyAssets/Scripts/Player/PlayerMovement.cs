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

    [SerializeField] public GameObject player;
    [SerializeField] public CharacterController controller;
    [SerializeField] public Animator animator;
    [SerializeField] public Camera cam;
    [SerializeField] public Interactable focus;  // Our current focus: Item, Enemy etc.
    [SerializeField] public Transform interactionTransform;  // The transform from where we interact in case you want to offset it
    [SerializeField] private LayerMask mask;
    [SerializeField] private float radius = 0.5f;               // How close do we need to be to interact?


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

    //Achievement
    public static event Action<string> PointOfInterest;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;
        cam = Camera.main;
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
                if (verticalInput > 0f){
                    animator.SetBool("running", true);
                    SoundManager.PlaySoundContinuously("Running");
                }
                else{
                    animator.SetBool("running", false);
                    if (SoundManager.isSoundPlaying){
                        SoundManager.PlaySoundStop();
                    }
                    
                }
                    

                moveVelocity = transform.forward * verticalInput;
                turnVelocity = transform.up * _rotationSpeed * horizontalInput;
                if (Input.GetButtonDown("Jump"))
                {
                    SoundManager.PlaySoundStop(); //Stop walking sound
                    SoundManager.PlaySound("Jump");
                    
                    //Achievement
                    PointOfInterest("Jump");

                    animator.SetTrigger("jumping");
                    moveVelocity.y = _jumpSpeed;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Collider[] itemsInRange = Physics.OverlapSphere(interactionTransform.position, 1.5f, mask);

                    foreach (Collider item in itemsInRange)
                    {
                        Interactable interactable = item.GetComponent<Interactable>();
                        if (interactable != null)
                        {
                            SetFocus(interactable);
                        }
                    }
                }

                moveVelocity.y += _gravity * Time.deltaTime;
                controller.Move(moveVelocity * Time.deltaTime);
                transform.Rotate(turnVelocity * Time.deltaTime);
            }

            moveVelocity.y += _gravity * Time.deltaTime;
            controller.Move(moveVelocity * _speed * Time.deltaTime);
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
