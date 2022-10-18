using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory/Item Database")]

public class ItemDatabaseObject : ScriptableObject
{
    public List<Item> Database;

    public Item FindItem(string itemName)
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
