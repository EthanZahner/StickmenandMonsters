using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Item Settings")]
    [SerializeField] private InventoryItem itemPrefab;  // Prefab for the inventory item
    [SerializeField] private Transform spawnParent;    // Parent for spawned items (e.g., inventory UI)
    [SerializeField] private InventorySlot[] inventorySlots; // Reference to available inventory slots

    [Header("Item Pool")]
    [SerializeField] private List<Equipment> itemPool; // Pool of available equipment items

    public static ItemSpawner Instance { get; private set; } // Singleton

    private void Awake()
    {
        // Ensure Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Spawns a random item from the item pool and assigns it to an inventory slot.
    /// </summary>
    public void SpawnRandomItem()
    {
        if (itemPool.Count == 0)
        {
            Debug.LogWarning("Item Pool is empty! Add items to the pool.");
            return;
        }

        // Pick a random item from the pool
        Equipment randomItem = itemPool[Random.Range(0, itemPool.Count)];

        // Find an empty slot
        foreach (var slot in inventorySlots)
        {
            if (slot.myItem == null)
            {
                // Spawn the item and assign it to the slot
                InventoryItem newItem = Instantiate(itemPrefab, slot.transform);
                newItem.Initialize(randomItem, slot);
                return;
            }
        }

        Debug.LogWarning("No empty inventory slots available!");
    }

    /// <summary>
    /// Spawns a specific item and assigns it to an inventory slot.
    /// </summary>
    public void SpawnSpecificItem(Equipment specificItem)
    {
        if (specificItem == null)
        {
            Debug.LogError("Specified item is null!");
            return;
        }

        // Find an empty slot
        foreach (var slot in inventorySlots)
        {
            if (slot.myItem == null)
            {
                // Spawn the item and assign it to the slot
                InventoryItem newItem = Instantiate(itemPrefab, slot.transform);
                newItem.Initialize(specificItem, slot);
                return;
            }
        }

        Debug.LogWarning("No empty inventory slots available!");
    }
}
