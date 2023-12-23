using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform InventoryParent; // Main Inventory
    InventoryManager inventory; // Inventory instance
    [SerializeField] InventorySlot[] slots;

    private void Start() 
    {
        InventoryParent = this.transform.GetChild(5).GetChild(1);
        slots = InventoryParent.GetComponentsInChildren<InventorySlot>();
        InventoryManager.instance.InventorySpace = slots.Length;
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
        if (InventoryManager.instance.ItemData.Count > 0)
        {
            List<Item> keyList = new List<Item>(InventoryManager.instance.ItemData.Keys); // List of the Items
            List<int> valueList = new List<int>(InventoryManager.instance.ItemData.Values); // List of Associated Values

            for (int i = 0; (i < slots.Length) && i < keyList.Count; i++)
            {
                slots[i].AddItem(keyList[i], valueList[i]);
            }
        }
        InventoryManager.instance.GoldCount.text = InventoryManager.instance.GoldAmount.ToString();
    }
}