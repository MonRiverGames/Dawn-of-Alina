using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDatabase : MonoBehaviour // Stores all items 
{
    #region Singleton
    public static InventoryDatabase database;

    private void Awake()
    {
        if (database != null)
        {
            return;
        }

        database = this;
    }
    #endregion // InventoryManager instance

    public List<Item> items;

    public Item GetItem(string itemName)
    {
        foreach(Item item in items)
        {
            if (item.Name == itemName)
            {
                return item;
            }
        }
        return null;
    }
}
