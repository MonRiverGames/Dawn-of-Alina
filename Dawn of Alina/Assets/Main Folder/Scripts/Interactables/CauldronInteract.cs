using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronInteract : Interactable
{
    public Item itemNeeded;
    public Item itemGiven;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact() // Adds Item to inventory upon interaction/pickup
    {
        if(InventoryManager.instance.ItemData.ContainsKey(itemNeeded) && (InventoryManager.instance.ItemData[itemNeeded] >= 3))
        {
            InventoryManager.instance.AddItem(itemGiven);
        }
        
        for (int i = 0; i < 3; i++)
        {
            InventoryManager.instance.Remove(itemNeeded);
        }

        InventoryManager.instance.UI.UpdateUI();
    }
}