using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.VFX;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform InventoryParent; // Parent gameobject of all inventory slots
    [SerializeField] InventorySlot[] slots; // Array of all the inventory slots in inventory 

    private void Start() 
    {
        InventoryParent = this.transform.GetChild(5).GetChild(1);
        slots = InventoryParent.GetComponentsInChildren<InventorySlot>();
        InventoryManager.instance.setInventorySpace(slots.Length);
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
            if (slot.item == item) // if the slot has an item in it
            {
                return true;
            }
        }
        return false;
    }

    public void UpdateUI() // actually Adds items to display on inventory screen
    {
        if (InventoryManager.instance.ItemData.Count > 0) // If there are items in the inventory to begin with
        {
            List<Item> keyList = new List<Item>(InventoryManager.instance.ItemData.Keys); // List of the Items
            List<int> valueList = new List<int>(InventoryManager.instance.ItemData.Values); // List of Associated Values

            for (int i = 0; (i < slots.Length) && i < keyList.Count; i++) // Loop through every inventory slot
            {
                if (!slots[i].isFilled && valueList[i] < keyList[i].stackLimit) // if the slot is not filled and does not exceed the stack limit
                {
                    slots[i].AddItem(keyList[i]); // add the item to the slot
                }
                else if (slots[i].isFilled && (valueList[i] < slots[i].item.stackLimit)) // if the slot is filled and does not exceed the stack limit
                {
                    slots[i].AddItem(keyList[i]); // add the item to the slot
                }
                else 
                {
                    slots[i].ClearSlot(); // make sure the slot remains empty
                }
            }
        }
    }
}