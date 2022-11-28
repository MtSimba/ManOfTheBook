using UnityEngine;

public class ItemPickUp : Interactable
{

    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.LogWarning("Picking up " + item.name);
        if(Inventory.instance.Add(item)) 
            Destroy(gameObject);
    }
}
