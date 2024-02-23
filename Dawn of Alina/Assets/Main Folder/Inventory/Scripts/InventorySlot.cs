using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour // Manages the info for each inventory slot
{
    [SerializeField] public Item item { get; private set; }
    [SerializeField] private InventoryUI UI;
    [SerializeField] private Transform ItemInfo; // Item info panel
    [SerializeField] private string ItemValue;
    [SerializeField] public bool isFilled {get; private set; } // If there is an item present in slot
    [SerializeField] public bool isViewing { get; private set; } // If player is looking at slot in inventory

    [SerializeField] private ShopInteract shop;
    [SerializeField] Button SellButton; // Sell button for selling items for gold
    [SerializeField] Sprite icon; // the icon for the item in the slot
    [SerializeField] Button RemoveButton; // button for removing 1 of an item from a slot

    public void AddItem(Item newItem) // Adds item to slot
    {
        this.item = newItem;
        this.icon = item.icon;
        int slotAmount = InventoryManager.instance.ItemData[newItem]; // number of the item in the slot
        transform.GetChild(0).gameObject.SetActive(true); // enables item icon for slot
        transform.GetChild(0).GetComponent<Image>().sprite = icon;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = slotAmount.ToString();
        SellButton.onClick.RemoveAllListeners(); 
        SellButton.onClick.AddListener(SellItem); // Adds sell button listener
        item.inSlot = true; // there is item in slot
        isFilled = true; // slot is filled
        UI.EnableRemoveButton(); // now displays remove button for slot

        if(slotAmount == 1)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false; // doesnt Display the amount
        }

        if (slotAmount > 1)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = true; // Displays the amount
        }
    }

    public void ClearSlot() // Clears a given slot
    {
        // Set all inventory slot features to null or disable them
        item.inSlot = false;
        item = null;
        icon = null;
        isFilled = false;
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = null;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false;
        CloseInfo();
        UI.EnableRemoveButton();
    }

    public void OnRemoveButton() // when the remove button of a slot is pressed
    {
        if (this.item != null && InventoryManager.instance.ItemData[this.item] >= 1)
        {
            InventoryManager.instance.DecrementItem(this.item, 1);

            if(!InventoryManager.instance.ItemData.ContainsKey(this.item)) {
                ClearSlot();
            }
        }

        UI.UpdateUI(); // update inventory UI
    }

    // On mouse enter 
    public void EnterItem() => isViewing = true; // is the mouse pointer in the inventory slot?

    // On mouse exit
    public void ExitItem() => isViewing = false; // did the mouse pointer exit the inventory slot?

    public void DisplayInfo() // Displays info on the item info panel such as the icon, description, the value of the item, and the sell button 
    {
        if (isViewing && isFilled)
        {
            ItemInfo.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.name;
            ItemInfo.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
            ItemInfo.GetChild(1).GetComponent<Image>().sprite = item.icon;
            ItemInfo.GetChild(1).GetComponent<Image>().enabled = true;
            ItemInfo.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.description;
            ItemInfo.GetChild(2).GetComponent<TextMeshProUGUI>().enabled = true;
            ItemValue = "Value: " + item.itemValue.ToString();
            ItemInfo.GetChild(3).GetComponent<TextMeshProUGUI>().text = ItemValue;
            ItemInfo.GetChild(2).GetComponent<TextMeshProUGUI>().enabled = true;
            ItemInfo.gameObject.SetActive(true);

            
           /*  Unfinished Shop
             if (shop.isShopActive)
            {
                ItemInfo.GetChild(4).gameObject.SetActive(true);
            }
            else
            {
                ItemInfo.GetChild(4).gameObject.SetActive(false);
            }*/
        }

        else
        {
            ItemInfo.GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
            ItemInfo.GetChild(1).GetComponent<Image>().sprite = null;
            ItemInfo.GetChild(2).GetComponent<TextMeshProUGUI>().text = null;
            ItemInfo.GetChild(3).GetComponent<TextMeshProUGUI>().text = null;
            ItemInfo.GetChild(4).gameObject.SetActive(false);
            ItemInfo.gameObject.SetActive(false);
        }
    }

    public void CloseInfo() => ItemInfo.gameObject.SetActive(false);


    public void SellItem() // When the player presses the sell button for an item
    {
        if (InventoryManager.instance.ItemData[item] > 0) // if the amount of the item > 0
        {
            InventoryManager.instance.setGoldAmount(InventoryManager.instance.GoldAmount + item.itemValue); // Add the value of the item to the player's gold
            OnRemoveButton(); // Remove 1 item from that slot
            UI.UpdateUI(); // Update Inventory UI
        }
        else // No items left
        {
            InventoryManager.instance.setGoldAmount(InventoryManager.instance.GoldAmount + item.itemValue); // Add the value of the item to the player's gold
            OnRemoveButton();
            ClearSlot();
            UI.UpdateUI();
        }
    }
}