using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory/Item Database")]

public class ItemDatabaseObject : ScriptableObject // Stores all of the in-game items for easy lookup in scripts
{
    public List<Item> Database; // List that stores eachs item

    public Item FindItem(string itemName) // Linear search for each item by name
    {
        foreach(Item item in Database)
        {
            if(item.Name == itemName)
            {
                return item;
            }
        }
        return null;
    }

}
