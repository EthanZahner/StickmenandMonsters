using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentSelectionMenu : MonoBehaviour
{
    [SerializeField]
    private Button[] buttonList; // Buttons in the UI for selecting equipment

    [SerializeField]
    private Equipment[] availableEquipment; // Available equipment items to display

    [SerializeField]
    private Inventory inventory; // Reference to the Inventory script

    [SerializeField]
    private GameObject panel; // Panel to close when selection is made

    private Equipment currentEquipment; // To store the current equipment

    void Start()
    {
        SetupButtons();
    }

    // This method will set up the buttons and assign the icon to each button based on the available equipment.
    public void SetupButtons()
    {


        for (int i = 0; i < buttonList.Length; i++)
        {
            Equipment currentItem = availableEquipment[i];
            Button button = buttonList[i];

            // Ensure the button has an Image component to set the icon
            Image buttonImage = button.GetComponent<Image>();
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

            if (buttonImage != null && currentItem != null && buttonText != null)
            {
                // Set the button's image icon to the current equipment's icon
                buttonImage.sprite = currentItem.icon;
                buttonImage.SetNativeSize(); // Optionally, set the image to its natural size

                // Set the button's text to the current equipment's name
                buttonText.text = currentItem.equipmentName;  // Here, we set the text to the equipment's name
            }
            else
            {
                Debug.LogError("Button Image component or Equipment is missing!");
            }

            // Add the button click listener
            button.onClick.RemoveAllListeners(); // Clear previous listeners
            button.onClick.AddListener(() => OnEquipmentSelected(currentItem));
        }
    }

    private void OnEquipmentSelected(Equipment selectedItem)
    {
        // Update current equipment to the selected item
        currentEquipment = selectedItem;

        Debug.Log($"Selected Item: {selectedItem.equipmentName}");

        if (selectedItem == null)
        {
            Debug.LogError("Selected equipment is null!");
            return;
        }

        // Ensure the inventory is assigned
        if (inventory != null)
        {
            inventory.SpawnInventoryItem(selectedItem);
            Debug.Log($"Added {selectedItem.equipmentName} to inventory!");
        }
        else
        {
            Debug.LogError("Inventory reference is missing!");
        }

        // Close the panel
        if (panel != null)
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel reference is null!");
        }
    }

    // Optional: A method to get the current equipment
    public Equipment GetCurrentEquipment()
    {
        return currentEquipment;
    }
}
