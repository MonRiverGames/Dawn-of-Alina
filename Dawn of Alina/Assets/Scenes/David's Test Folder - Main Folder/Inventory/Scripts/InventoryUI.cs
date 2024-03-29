using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform InventoryParent; // Main Inventory
    InventoryManager inventory; // Inventory instance
    InventorySlot[] slots;
    public RHandSlot RHand;
    public LHandSlot LHand;
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
        if (LHand.isFilled)
        {
            LHand.transform.GetChild(1).gameObject.SetActive(true); // Enable Remove Button
        }
        else
        {
            LHand.transform.GetChild(1).gameObject.SetActive(false); // Disable Remove Button
        }

        if (RHand.isFilled)
        {
            RHand.transform.GetChild(1).gameObject.SetActive(true); // Enable Remove Button
        }
        else
        {
            RHand.transform.GetChild(1).gameObject.SetActive(false); // Disable Remove Button
        }


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

        if (inventory.EquippedItems[0].type == ItemType.Equipment)
        {
            print("Equip Left");
            LHand.AddItem(inventory.EquippedItems[0]);
            LHand.item.inSlot = true;
        }

        if (inventory.EquippedItems[1].type == ItemType.Equipment) // RIGHT
        {
            print("Equip Right");
            RHand.AddItem(inventory.EquippedItems[1]);
            RHand.item.inSlot = true;
        }
    }
}