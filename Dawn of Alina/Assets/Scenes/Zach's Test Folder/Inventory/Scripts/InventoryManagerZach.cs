using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerZach : MonoBehaviour // Manages inventory as a Singleton
{
    #region Singleton
    public static InventoryManagerZach instance;

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion // InventoryManager instance

    public List<Item> items = new List<Item>(); // Holds items; basically the inventory
    public int InventorySpace = 30; // # of slots in hotbar
    public int HotbarSpace = 5; // # of slots in hotbar
    public InventoryUIZach UI;

    public void AddItem(Item item)
    {
        if (!items.Contains(item)) // If Item is not in inventory
        {
            if (items.Count >= InventorySpace)
            {
                Debug.Log("Inventory Full");
                return;
            }
            items.Add(item);
        }

        else // Item is already in inventory
        {
            item.amount++; // Increase Item amount
        }

        UI.UpdateUI(); // Update Inventory Screen
    }
}