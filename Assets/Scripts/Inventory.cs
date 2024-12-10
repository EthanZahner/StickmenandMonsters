using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;

    [SerializeField]
    InventorySlot[] inventorySlots;

    [SerializeField]
    Transform draggablesTransform;
    [SerializeField]
    InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField]
    Equipment[] items;

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    // Store previously equipped items to subtract their bonuses when unequipped
    private Dictionary<SlotTag, Equipment> equippedItems = new Dictionary<SlotTag, Equipment>();

    private void Awake()
    {
        Singleton = this;
        giveItemBtn.onClick.AddListener(delegate { SpawnInventoryItem(); });
    }

    public void SpawnInventoryItem(Equipment item = null)
    {
        if (inventorySlots == null || inventorySlots.Length == 0)
        {
            Debug.LogError("Inventory slots array is null or empty!");
            return;
        }

        Equipment _item = item;
        if (_item == null)
        {
            int random = Random.Range(0, items.Length);
            _item = items[random];
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == null)
            {
                Debug.LogError($"Inventory slot at index {i} is null!");
                continue;
            }

            if (inventorySlots[i].myItem == null)
            {
                InventoryItem newItem = Instantiate(itemPrefab, inventorySlots[i].transform);
                if (newItem != null)
                {
                    newItem.Initialize(_item, inventorySlots[i]);
                }
                else
                {
                    Debug.LogError("Failed to instantiate itemPrefab!");
                }
                break;
            }
        }
    }


    private void Update()
    {
        if (carriedItem == null) return;

        carriedItem.transform.position = Input.mousePosition;
    }

    public void SetCarriedItem(InventoryItem item)
    {
        // If we're holding an item (not null), unequip the current item from the slot it was in
        if (carriedItem != null)
        {
            // Remove stats for the item that's being swapped out
            EquipEquipment(carriedItem.activeSlot.myTag, null);

            // Swap items between slots (move carried item into the target slot)
            item.activeSlot.SetItem(carriedItem);
        }
        else
        {
            // If no item is being carried, we must unequip the current item in the slot
            EquipEquipment(item.activeSlot.myTag, null);
            carriedItem = item;
            carriedItem.canvasGroup.blocksRaycasts = false;
            item.transform.SetParent(draggablesTransform);  // Allow item to be dragged around
        }

        // Set the new item as the carried item (if not null)
        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);  // Allow item to be dragged around
    }

    public void EquipEquipment(SlotTag tag, InventoryItem item = null)
    {
        FighterStats playerStats = FindObjectOfType<FighterStats>();

        // Check if there is an item currently equipped in this slot
        if (equippedItems.ContainsKey(tag))
        {
            var currentEquippedItem = equippedItems[tag];

            // Remove stat bonuses for the currently equipped item
            ApplyStatBonuses(-currentEquippedItem.attackBonus, -currentEquippedItem.spellAttackBonus, -currentEquippedItem.manaBonus,
                             -currentEquippedItem.defenseBonus, -currentEquippedItem.speedBonus, -currentEquippedItem.healthBonus);
            Debug.Log($"Unequipped {currentEquippedItem.equipmentName} from {tag}");

            // Remove the item from the dictionary
            equippedItems.Remove(tag);
        }

        // If there’s a new item to equip (if item is not null)
        if (item != null)
        {
            var newItem = item.myItem;

            // Store the new item as the equipped item for this slot
            equippedItems[tag] = newItem;

            // Apply the stat bonuses from the newly equipped item
            ApplyStatBonuses(newItem.attackBonus, newItem.spellAttackBonus, newItem.manaBonus,
                             newItem.defenseBonus, newItem.speedBonus, newItem.healthBonus);
            Debug.Log($"Equipped {newItem.equipmentName} on {tag}");
        }
        else
        {
            // If no item is passed in, we treat this as unequipping an item (reset stats)
            Debug.Log($"No item to equip in {tag}");
        }

        // Log the updated stats
        FighterStats stats = FindObjectOfType<FighterStats>();
        Debug.Log($"New Stats: Attack: {stats.attack}, Spell Attack: {stats.spellAttack}, Mana: {stats.mana}, " +
                  $"Defense: {stats.defense}, Speed: {stats.speed}, Health: {stats.health}");

        StatsUpdater statsUpdater = FindObjectOfType<StatsUpdater>();
        statsUpdater.OnStatsChanged();  // Notify that stats have changed

    }


    private void ApplyStatBonuses(float attack, float spellAttack, float mana, float defense, float speed, float health)
    {
        // Adjust the player's stats based on the item being equipped
        FighterStats playerStats = FindObjectOfType<FighterStats>();  // Assuming you have a FighterStats script

        playerStats.attack += attack;
        playerStats.spellAttack += spellAttack;
        playerStats.mana += mana;
        playerStats.defense += defense;
        playerStats.speed += speed;
        playerStats.startHealth += health;
    }
}
