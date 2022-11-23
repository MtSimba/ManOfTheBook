using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Object")]

public class ItemObject : Item
{
    public SkinnedMeshRenderer mesh;

    // Called when pressed in the inventory
    public override void Use()
    {
        
    }

}
