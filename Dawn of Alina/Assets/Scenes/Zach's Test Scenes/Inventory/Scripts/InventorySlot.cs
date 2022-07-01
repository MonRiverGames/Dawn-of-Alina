using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour // Manages the info for each inventory slot
{
    public Item item; 
    public Sprite icon;
    InventoryManager inventory;

    public void AddItem(Item newItem) // Adds item to slot
    {
        item = newItem;
        icon = item.icon;
        transform.GetChild(0).GetComponent<Image>().sprite = icon;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.amount.ToString();
        
        if (newItem.amount > 1)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = true; // Displays the amount
        }

    }

    public void ClearSlot() // Clears a given slot
    {
        item = null;
        icon = null;
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = null;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false;
    }
}