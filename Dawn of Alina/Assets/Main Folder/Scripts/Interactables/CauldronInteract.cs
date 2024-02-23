using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronInteract : Interactable
{
    private InventoryUI UI;
    public Item itemNeeded; // Mushroooms;
    public Item itemGiven; // Potion;

    private void Start()
    {
        UI = GameObject.Find("PlayerUI").GetComponent<InventoryUI>();
    }


    protected override void Interact() // Adds Item to inventory upon interaction/pickup
    {
        if(InventoryManager.instance.ItemData.ContainsKey(itemNeeded) && (InventoryManager.instance.ItemData[itemNeeded] >= 2))
        {
            InventoryManager.instance.AddItem(itemGiven, 1);
            InventoryManager.instance.DecrementItem(itemNeeded, 2);
            UI.UpdateUI();
        }

        else
        {
            Debug.Log("Cannot Craft item");
        }

       
    }
}