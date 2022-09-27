using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public Transform ShopParent; // Main Inventory
    InventoryManager inventory; // Inventory instance
    ShopManager shop; // Shop instance
    ShopItem[] slots;
    public GameObject Player;
    PlayerLook playerLook;

    void Start()
    {
        inventory = InventoryManager.instance;
        slots = ShopParent.GetComponentsInChildren<ShopItem>();
        inventory.InventorySpace = slots.Length;
        playerLook = Player.GetComponent<PlayerLook>();
    }

    public void EnableRemoveButton() // Enables/Disables remove button on inventoy slot
    {
        foreach (ShopItem slot in slots)
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
        foreach (ShopItem slot in slots)
        {
            if (slot.item = item)
            {
                return true;
            }
        }
        return false;
    }

    public void UpdateUI() // Adds items to inventory screen
    {
        List<Item> keyList = new List<Item>(inventory.ItemData.Keys);

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.ItemData.Count)
            {
                if (!slots[i].isFilled || (inventory.ItemData[slots[i].item] <= slots[i].item.stackLimit) && slots[i].item.inSlot)
                {
                    slots[i].AddItem(keyList[i]);
                    slots[i].item.inSlot = true;
                }
            }
        }
    }
}
