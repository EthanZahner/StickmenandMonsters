using UnityEngine;

public enum SlotTag { None, Weapon, Magic, Ring, Amulet, Helm, Chestplate, Pants, Boots }

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Equipment/EquipmentItem")]
public class Equipment : ScriptableObject
{
    public string equipmentName;    // Name of the equipment
    public Sprite icon;             // Icon to display in UI 
    public string rarity;           //"common", "uncommon", "rare", "epic", "legendary", "mythical
    public int purchasePrice;       // Gold cost to purchase the item
    public int sellPrice;           // Gold price of sellling the item
    public SlotTag itemTag;

    [Header("Stat Modifiers")]
    public float attackBonus;       // Adds to attack stat
    public float spellAttackBonus;  //Adds to the spell attack bonus
    public float manaBonus;         //Adds to the mana stat
    public float defenseBonus;      // Adds to defense stat
    public float speedBonus;        // Adds to speed stat
    public float healthBonus;       // Adds to health stat

    [TextArea]
    public string description;      // Description for tooltips
}
