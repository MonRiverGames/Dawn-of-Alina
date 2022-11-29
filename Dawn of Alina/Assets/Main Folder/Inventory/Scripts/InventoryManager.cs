using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

[System.Serializable]
public class InventoryManager : MonoBehaviour// Manages inventory as a Singleton
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

    public Dictionary<Item,int> ItemData = new Dictionary<Item,int>(); // Holds Items and their corresponding amount
    public List<Item> ItemList = new List<Item>();
    public int InventorySpace = 25;
    public InventoryUI UI;
    private PlayerHealth PlayerData;
    public ItemDatabaseObject Database;
    public string SavePath = "/inventoryItems";
    public GameObject Player;
    public TextMeshProUGUI GoldCount;
    public int GoldAmount = 1000;
    
    public void Start()
    {
        Item Default = ScriptableObject.CreateInstance<Item>();
        PlayerData = PlayerHealth.instance;
        Default.type = ItemType.Default;
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
            ItemList.Add(item);
        }

        else // Item is already in inventory
        {
            ItemData.TryAdd(item, ItemData[item]++); // Increase Item amount
        }
        UI.UpdateUI(); // Update Inventory Screen
    }

    public void DecrementItem(Item item, int amount) // Decrases item amount by amount specific
    {
        if (!ItemData.ContainsKey(item)) // If Item is not in inventory
        {
            Debug.Log("NOT IN INVENTORY");
            return;
        }

        else // Item is already in inventory
        {
          for(int i = 0; i < amount; i++)
            {
                if(ItemData[item] == 1)
                {
                    ItemData.Remove(item);
                    break;
                }
                else
                {
                    ItemData[item]--;
                }
            }
            
        }
        UI.UpdateUI(); // Update Inventory Screen
    }

    public void Remove(Item item) // Removes item from inventory
    {
        ItemData.Remove(item);
        UI.UpdateUI();
    }

    [ContextMenu("Save")]
    public void Save(InventoryManager inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, SavePath + ".savedata"), FileMode.Create, FileAccess.Write);
        SaveData data = new SaveData();
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Items and amounts Saved");
    }

    [ContextMenu("Load")]
    public SaveData LoadData()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, SavePath + ".savedata")))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, SavePath + ".savedata"), FileMode.Open, FileAccess.Read);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            Debug.Log("Loaded SaveData");
            return data;
        }
        else
        {
            Debug.Log("ERROR CANNOT LOAD SAVEDATA");
            return null;
        }
    }
    public void UpdateInventory(SaveData data)
    {
        for (int i = 0; i < data.itemNames.Length; i++)
        {
            for (int j = 0; j < data.itemAmounts[i]; j++)
            {
                AddItem(Database.FindItem(data.itemNames[i]));
            }
        }

        UI.UpdateUI();
        Debug.Log(data.playerHealth);
        PlayerData.health = data.playerHealth;
        Vector3 position;
        position.x = data.playerPos[0];
        position.y = data.playerPos[1];
        position.z = data.playerPos[2];
        Player.transform.position = position;
        Debug.Log("X: " + data.playerPos[0]);
        Debug.Log("Y: " + data.playerPos[1]);
        Debug.Log("Z: " + data.playerPos[2]);   
    }
}