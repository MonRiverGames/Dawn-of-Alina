using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform InventoryParent; // Main Inventory
    InventoryManager inventory; // Inventory instance
    InventorySlot[] slots;
    public GameObject Player;
    PlayerLook playerLook;
    public TextMeshProUGUI GoldText;

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
        foreach (InventorySlot slot in slots)
        {
            if (slot.item = item)
            {
                return true;
            }
        }
        return false;
    }

    public void UpdateUI() // actually Adds items to display on inventory screen
    {
        List<Item> keyList = new List<Item>(inventory.ItemData.Keys);

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.ItemData.Count)
            {
                if (!slots[i].isFilled || (inventory.ItemData[slots[i].item] <= slots[i].item.stackLimit && slots[i].item.inSlot))
                {
                    slots[i].AddItem(keyList[i]);
                    slots[i].item.inSlot = true;

                    if (InventoryManager.instance.ItemData[slots[i].item] == 1)
                    {
                        slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = null;
                    }

                    if (InventoryManager.instance.ItemData[slots[i].item] <= 0 )
                    {
                        InventoryManager.instance.Remove(slots[i].item);
                    }
                }
            }
        }

       InventoryManager.instance.GoldCount.text = inventory.GoldAmount.ToString();
    }
}