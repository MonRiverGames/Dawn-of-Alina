using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform InventoryParent; // Main Inventory
    InventoryManager inventory; // Inventory instance
    InventorySlot[] slots;
    public GameObject Player;
    PlayerLook playerLook;

    void Start()
    {
        inventory = InventoryManager.instance;
        slots = InventoryParent.GetComponentsInChildren<InventorySlot>();
        inventory.InventorySpace = slots.Length;
        playerLook = Player.GetComponent<PlayerLook>();
    }

    public void EnableRemoveButton() // Enables/Disables remove button on inventoy slot
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.isFilled)
            {
                slot.transform.GetChild(2).gameObject.SetActive(true); // Enable Remove Button
            }
            else
            {
                slot.transform.GetChild(2).gameObject.SetActive(false); // Disable Remove Button
            }
        }
    }

    public bool inSlot(Item item) // Checks if item is present in inventory slot
    {
        foreach(InventorySlot slot in slots)
        {
            if(slot.item = item)
            {
                return true;
            }
        }
        return false;
    }

    public void UpdateUI() // Adds items to inventory screen
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                if (!slots[i].isFilled || (slots[i].item.amount <= slots[i].item.stackLimit) && slots[i].item.inSlot)
                {
                    slots[i].AddItem(inventory.items[i]);
                    slots[i].item.inSlot = true;
                }
            }
        }
    }
}