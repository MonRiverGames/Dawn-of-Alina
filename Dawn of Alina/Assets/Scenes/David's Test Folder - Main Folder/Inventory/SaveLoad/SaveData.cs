using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string[] itemNames = new string[InventoryManager.instance.ItemData.Count];
    public int[] itemAmounts = new int[InventoryManager.instance.ItemData.Count];

    public SaveData ()
    {
        InventoryManager inventory = InventoryManager.instance;

        foreach(KeyValuePair<Item, int> pair in inventory.ItemData)
        {
            for(int i = 0; i < inventory.ItemData.Count; i++)
            {
                itemAmounts[i] = pair.Value;
                itemNames[i] = pair.Key.Name;
            }
        }
    }
}
