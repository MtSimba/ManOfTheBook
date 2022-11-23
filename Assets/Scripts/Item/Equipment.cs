using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
	// Start is called before the first frame update
	public EquipmentSlot equipSlot;     // What slot to equip it in
    public int armorModifier;
    public int damageModifier;
    public SkinnedMeshRenderer mesh;

    // Called when pressed in the inventory
    public override void Use()
    {
        EquipmentManager.instance.Equip(this);  // Equip
        RemoveFromInventory();  // Remove from inventory
    }

}
public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }
