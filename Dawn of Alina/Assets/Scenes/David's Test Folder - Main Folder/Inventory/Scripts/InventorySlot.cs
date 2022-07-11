using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour // Manages the info for each inventory slot
{
    public Item item; 
    public Sprite icon;
    public Button RemoveButton;
    public InventoryUI UI;
    public bool isFilled;

    public void AddItem(Item newItem) // Adds item to slot
    {
        item = newItem;
        
        if (newItem.amount <= newItem.stackLimit)
        {
            icon = item.icon;
            transform.GetChild(0).GetComponent<Image>().sprite = icon;
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.amount.ToString();
            isFilled = true;
        }
        else 
        {
            newItem.amount = 1;
            InventoryManager.instance.AddItem(newItem);
        }

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
        isFilled = false;
    }

    public void OnRemoveButton()
    {
        Debug.Log("HELLO THERE");
        InventoryManager.instance.Remove(item);
        ClearSlot();
        UI.UpdateUI();
    }
}