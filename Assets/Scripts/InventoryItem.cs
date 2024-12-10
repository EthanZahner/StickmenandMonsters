using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    public Image itemIcon;
    public CanvasGroup canvasGroup { get; private set; }

    public Equipment myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();  // Assign Image component
    }

    public void Initialize(Equipment item, InventorySlot parent)
    {
        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;

        // Log to check if the item is initialized properly
        if (item.icon != null)
        {
            Debug.Log("Icon is set for item: " + item.equipmentName + itemIcon);
            itemIcon.sprite = item.icon;
        }
        else
        {
            Debug.LogWarning("Item icon is missing for " + item.equipmentName);
        }
    }



public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Singleton.SetCarriedItem(this);
        }
    }
}
