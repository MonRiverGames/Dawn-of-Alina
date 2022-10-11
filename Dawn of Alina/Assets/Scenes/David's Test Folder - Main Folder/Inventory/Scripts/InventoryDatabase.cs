using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDatabase : MonoBehaviour
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
        foreach(var item in items)
        {
            if (item.Name == itemName)
            {
                return item;
            }
        }
        return null;
    }
}
