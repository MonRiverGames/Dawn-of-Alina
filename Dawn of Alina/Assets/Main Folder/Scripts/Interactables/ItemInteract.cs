using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : Interactable
{
    [SerializeField] private Item item;

    protected override void Interact() // Adds Item to inventory upon interaction/pickup
    {
        InventoryManager.instance.AddItem(item,1);
        Destroy(gameObject);
    }
}