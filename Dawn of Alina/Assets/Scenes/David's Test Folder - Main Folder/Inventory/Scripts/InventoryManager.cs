using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour // Manages inventory as a Singleton
{
    #region Singleton
    public static InventoryManager instance;

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
    public int InventorySpace = 25; // # of slots
    public InventoryUI UI;

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

    public void Remove(Item item)
    {
        items.Remove(item);
    }
}