using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
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

    public List<Item> EquippedItems = new List<Item>(); // Holds Equipped Items
    public Dictionary<Item,int> ItemData = new Dictionary<Item,int>(); // Holds Items and their corresponding amount
    public int InventorySpace = 25;
    public InventoryUI UI;
    public ItemDatabaseObject Database;
    public string ItemSavePath = "/inventoryItems";
    
    public void Start()
    {
        Item Default = ScriptableObject.CreateInstance<Item>();
        Default.type = ItemType.Default;
        EquippedItems = new List<Item>{Default, Default};
    }

    public void AddItem(Item item)
    {
        if (!ItemData.ContainsKey(item)) // If Item is not in inventory
        {
            if (ItemData.Count >= InventorySpace)
            {
                Debug.Log("Inventory Full");
                return;
            }
            ItemData.Add(item,1);
        }

        else // Item is already in inventory
        {
            ItemData.TryAdd(item, ItemData[item]++); // Increase Item amount
        }

        UI.UpdateUI(); // Update Inventory Screen
    }

    public void EquipLeftItem(Item item)
    {
        if (EquippedItems[0].type != ItemType.Default) // Has Item So Unequip
        {
            Item Default = ScriptableObject.CreateInstance<Item>();
            Default.type = ItemType.Default;
            EquippedItems[0] = Default;
            AddItem(item);
            UI.UpdateUI();
        }
        else
        {
            if (item.type == ItemType.Equipment)
            {
                EquippedItems[0] = item;
                UI.UpdateUI();
            }
        }
    }

    public void EquipRightItem(Item item)
    {  
        if (item.type == ItemType.Equipment)
        {
            EquippedItems[1] = item;
            UI.UpdateUI();
        }
    }

    public void UnequipLeft()
    {
        if (EquippedItems[0].type != ItemType.Default) // Has Item So Unequip
        {
            Item Default = ScriptableObject.CreateInstance<Item>();
            Default.type = ItemType.Default;
            AddItem(EquippedItems[0]);
            EquippedItems[0] = Default;
            UI.UpdateUI();
        }
    }

    public void UnequipRight()
    {
        if (EquippedItems[1].type != ItemType.Default) // Has Item So Unequip
        {
            Item Default = ScriptableObject.CreateInstance<Item>();
            Default.type = ItemType.Default;
            AddItem(EquippedItems[1]);
            EquippedItems[1] = Default;
            UI.UpdateUI();
        }
    }

    public void Remove(Item item) // Removes item from inventory
    {
        ItemData.Remove(item);
    }


    [ContextMenu("Save")]
    public void Save()
    {
        string DataString = "";

            foreach (Item item in ItemData.Keys)
            {
                foreach (int amount in ItemData.Values)
                {
                    Debug.Log(item.name);
                    DataString = string.Concat(DataString, item.name + "/" );
                    DataString = string.Concat(DataString, amount + "&");
                    Debug.Log("Data String" + " is " + DataString);
                }
            }

        Debug.Log(DataString);
        File.WriteAllText(string.Concat(Application.persistentDataPath, ItemSavePath + ".txt"), DataString);
        Debug.Log("Items and amounts Saved");
    }

    [ContextMenu("Load")]
    public void Load()
    {
        string DataString = "";

        if (File.Exists(string.Concat(Application.persistentDataPath, ItemSavePath + ".txt")))
        {
            DataString = File.ReadAllText(string.Concat(Application.persistentDataPath, ItemSavePath + ".txt"));

            Debug.Log("Items Loaded");
            string[] items = DataString.Split('/');

            for(int i = 0; i < items.Length; i++)
            {
                DataString = items[i].Trim('&');
                
                if(i % 2 == 0) // Then we know is item
                {
                    int amountToAdd = int.Parse(items[i + 1].Trim('&'));
                    print(amountToAdd);
                    for (int j = 1; j <= amountToAdd; j++)
                    {
                        AddItem(Database.FindItem(items[i]));
                    }
                    
                }
            }

        }

    }
}