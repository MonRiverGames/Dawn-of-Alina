using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronInteract : Interactable
{
    public Item itemNeeded; // Mushroooms;
    public Item itemGiven; // Potion;


    protected override void Interact() // Adds Item to inventory upon interaction/pickup
    {
        if(InventoryManager.instance.ItemData.ContainsKey(itemNeeded) && (InventoryManager.instance.ItemData[itemNeeded] >= 2))
        {
            InventoryManager.instance.AddItem(itemGiven);

            InventoryManager.instance.DecrementItem(itemNeeded, 2);
            InventoryManager.instance.UI.UpdateUI();
        }

       
    }
}