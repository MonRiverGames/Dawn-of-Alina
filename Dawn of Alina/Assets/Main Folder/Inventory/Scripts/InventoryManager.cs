using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class InventoryManager : MonoBehaviour // Manages inventory as a Singleton
{
    #region Singleton Pattern
    public static InventoryManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion // InventoryManager instance 

    [field: SerializeField] public Dictionary<Item, int> ItemData = new Dictionary<Item, int>(); // Holds Items and their corresponding amount
    [field: SerializeField] public List<string> ItemNames = new List<string>(); 
    [field: SerializeField] public int InventorySpace { get; private set; } // Amount of inventory slots
    [field: SerializeField] public ItemDatabaseObject Database { get; private set; } // Reference to database containing all items
    [field: SerializeField] public TextMeshProUGUI GoldCount { get; private set; } // Text representation of the amount of gold a player has
    [field: SerializeField] public int GoldAmount { get; private set; } // int representation of the amount of gold a player has

    [field: SerializeField] private PlayerHealth PlayerData; // Reference to a player's savedata

    [field: SerializeField] private string SavePath = "/inventoryItems"; // file path for where to save data

    [field: SerializeField] private InventoryUI inventoryUI; // Reference to InventoryUI Script
    [field: SerializeField] public List<Item> keyList { get; private set; }  // List of the Items
    [field: SerializeField] public List<int> valueList  {get; private set; } // List of Associated Values
     
    public void Start()
    {
        PlayerData = PlayerHealth.instance;
        inventoryUI = GameObject.Find("PlayerUI").GetComponent<InventoryUI>();
        keyList = ItemData.Keys.ToList();
        valueList = ItemData.Values.ToList();
    }

    // Public setter for setting inventory space
    public void setInventorySpace(int inventorySpace) => InventorySpace = inventorySpace;

    // Public setter for setting gold amount
    public void setGoldAmount(int goldAmount)
    {
        GoldAmount = goldAmount;
        GoldCount.text = GoldAmount.ToString();
    }

    public void AddItem(Item item, int amount) // Adds an item to the inventory dictionary
    {
        if(item.Name == "Gold")
        {
            setGoldAmount(GoldAmount + item.itemValue);
        }

        if (!ItemData.ContainsKey(item)) // If Item is not in inventory
        {
            if (ItemData.Count >= InventorySpace)
            {
                Debug.Log("Inventory Full");
                return;
            }
            ItemData.Add(item, amount); // Add amount of the item to the inventory / ItemData Dictionary
        }

        else // Item is already in inventory
        {
            if (ItemData[item] < item.stackLimit) // if the amount of the item in ItemData hasn't reached the stack limit
            {
                ItemData[item] += amount; // Add the item to the inventory
            }
            else
            {
                Debug.LogError("Max item amount reached. Cannot add item");
            }
        }
        keyList = new List<Item>(InventoryManager.instance.ItemData.Keys);
        valueList = new List<int>(InventoryManager.instance.ItemData.Values);

        if (!ItemNames.Contains(item.Name))
        {
            ItemNames.Add(item.Name);
        }

        inventoryUI.UpdateUI();
    }

    public void DecrementItem(Item item, int amount) // Decreases item amount by amount specific
    {
        if (!ItemData.ContainsKey(item)) // If Item is not in inventory
        {
            Debug.LogError("Cannot decrement item. Item not in Inventory");
            return;
        }

        else // Item is already in inventory
        {
          for(int i = 0; i < amount; i++)
            {
                if(ItemData[item] <= 1) // if only 1 item left in inventory
                {
                    if(item.Name == "Gold" ) // if gold then remove from players gold amount too
                    {
                        setGoldAmount(GoldAmount - item.itemValue);
                    }
                    
                    ItemData.Remove(item); // Completely remove from inventory
                }
                else
                {
                    ItemData[item]--;

                    if (item.Name == "Gold") // if gold then remove from players gold amount too
                    {
                        setGoldAmount(GoldAmount - item.itemValue);
                    }

                    inventoryUI.UpdateUI(); // Update Inventory Screen
                }
            }   
        }
        
        keyList = new List<Item>(InventoryManager.instance.ItemData.Keys); // update list of items to view in inspector
        valueList = new List<int>(InventoryManager.instance.ItemData.Values); // update list of cooresponding item amounts to view in inspector
    }

    public void Remove(Item item) // Removes item completely from inventory
    {
        ItemData.Remove(item); 
        ItemNames.Remove(item.Name);
        keyList = new List<Item>(InventoryManager.instance.ItemData.Keys); // update list of items to view in inspector
        valueList = new List<int>(InventoryManager.instance.ItemData.Values); // update list of cooresponding item amounts to view in inspector
        inventoryUI.UpdateUI(); // Update Inventory Screen
    }

    [ContextMenu("Save")]
    public void Save(InventoryManager inventory) // Saves Inventory/Player Data as .savedata file
    {
        // create the file path for the save data
        BinaryFormatter formatter = new BinaryFormatter(); // Used to serialize data
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, SavePath + ".savedata"), FileMode.Create, FileAccess.Write);
        SaveData data = new SaveData(ItemNames.ToArray(), ItemData.Values.ToArray());
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Inventory Data Saved");
    }

    [ContextMenu("Load")]
    public SaveData LoadData() // Loads Inventory/Player Data from existing save data file
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, SavePath + ".savedata"))) // If a file has been saved previously
        {
            IFormatter formatter = new BinaryFormatter();
            // Read in the file
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, SavePath + ".savedata"), FileMode.Open, FileAccess.Read);
            SaveData data = formatter.Deserialize(stream) as SaveData; 
            stream.Close();
            Debug.Log("Loaded in Save Data");
            return data;
        }
        else
        {
            Debug.LogError("ERROR: Cannot load save data from file. Save data cannot be found");
            return null;
        }
    }
    public void UpdateInventory(SaveData data) // Updates inventory with data from loaded .savedata file
    {
        // Double for loop iterates through each item in the save data, and then the cooresponding amount for each item in the inventory
        Debug.Log("Length = " + data.itemNames.Length);
        for(int i = 0; i < data.itemNames.Length; i++)
        {
            AddItem(Database.FindItem(data.itemNames[i]), data.itemAmounts[i]);
        }

        // Updates the players health and position from save data
        PlayerData.health = data.playerHealth;
        Vector3 position; // Vector3  position of player in world
        position.x = data.playerPos[0];
        position.y = data.playerPos[1];
        position.z = data.playerPos[2];
        transform.position = position; // set the players position
    }
}