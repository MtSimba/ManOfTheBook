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
    private float turnVelocity;

    private bool isGrounded;

    //Achievement
    public static event Action<string> PointOfInterest;

    void Start()
    {

        cam = Camera.main;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (!instance.GetComponent<PlayerCombat>().isDead())
        {

            interact();

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            animator.SetFloat("Horizontal", move.x);
            animator.SetFloat("Vertical", move.z);

            handleRotation(move);

            if (isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    animator.SetTrigger("roll_left");
                }

                if (Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    animator.SetTrigger("roll_right");
                }

                if (move != Vector3.zero)
                {
                    animator.SetBool("running", true);
                    SoundManager.PlaySoundContinuously("Running");
                }
                else
                {
                    animator.SetBool("running", false);
                    if (SoundManager.isSoundPlaying)
                    {
                        SoundManager.PlaySoundStop();
                    }
                }

                
                if (Input.GetButtonDown("Jump"))
                {
                    isGrounded = false;
                    SoundManager.PlaySoundStop(); //Stop walking sound
                    SoundManager.PlaySound("Jump");
                    
                    //Achievement
                    PointOfInterest("Jump");

                    animator.SetTrigger("jumping");
                    move.y = _jumpSpeed;
                }

                move.y += _gravity * Time.deltaTime;
                Vector3 cameraRelativeMovementJump = covertToCameraSpace(move);
                controller.Move(cameraRelativeMovementJump * Time.deltaTime);
               
            }

            move.y += _gravity * Time.deltaTime;
            Vector3 cameraRelativeMovement = covertToCameraSpace(move);
            controller.Move(cameraRelativeMovement * _speed * Time.deltaTime);
            //transform.Rotate(turnVelocity * Time.deltaTime);
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

    Vector3 covertToCameraSpace(Vector3 vectorToRotate)
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightZProdcut = vectorToRotate.x * cameraRight;

        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightZProdcut;
        return vectorRotatedToCameraSpace;
        
    }

    void handleRotation(Vector3 direction)
    {
        float targetAngle = MathF.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, 0.1f);
        transform.rotation = Quaternion.Euler(0f,angle,0f) ;
    }

    void interact()
    {
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
    }

    // Remove our current focus
    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
    }
}
