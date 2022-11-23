using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Singleton
    [SerializeField]
    public static CharacterMovement instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] public Animator animator;
    [SerializeField] public CharacterController controller;
    [SerializeField] public Interactable focus;  // Our current focus: Item, Enemy etc.
    [SerializeField] public Transform cam;         // Reference to our camera
    [SerializeField] public Transform interactPoint;
    [SerializeField] public LayerMask items;


    public float speed = 6f;
    private float turnSmoothVelocity;


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        animator.SetFloat("InputX",horizontal);
        animator.SetFloat("InputY", vertical);

        Vector3 direction = new Vector3(horizontal,0,vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            animator.SetBool("running", true);

            if (Input.GetMouseButton(1))
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f );
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else
            {
                controller.Move(direction * speed * Time.deltaTime);
            }

        }
        else
        {
            animator.SetBool("running", false);
        }

        // If we press E
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] itemsInRange = Physics.OverlapSphere(interactPoint.position, 1.5f, items);

           foreach(Collider item in itemsInRange) 
           {
                Interactable interactable =item.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

    }

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
