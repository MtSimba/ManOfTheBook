using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float radius = 0.5f;               // How close do we need to be to interact?
    [SerializeField] private Transform interactionTransform;  // The transform from where we interact in case you want to offset it
    [SerializeField] private LayerMask mask;

    private readonly Collider[] colliders = new Collider[3];
    private int numFound;

    bool isFocus = false;   // Is this interactable currently being focused?
    Transform player;       // Reference to the player transform

    bool hasInteracted = false; // Have we already interacted with the object?

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        //Debug.Log("Interacting with " + transform.name);
    }

    void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionTransform.position, radius, colliders, mask);


    }
}
