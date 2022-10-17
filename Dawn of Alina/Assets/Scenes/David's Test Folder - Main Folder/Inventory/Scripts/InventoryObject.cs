using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory System/Inventory")]

public class InventoryObject : ScriptableObject 
{
    InventoryManager Inventory;
    public string SavePath;

    [ContextMenu("Save")]
    public void Save()
    {
        string saveData = JsonUtility.ToJson(Inventory);
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Path.Combine(Application.persistentDataPath, SavePath + ".json"),FileMode.Create,FileAccess.Write);
        formatter.Serialize(stream, Inventory);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, SavePath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(Application.persistentDataPath, SavePath + ".json"), FileMode.Open, FileAccess.Read);
            Inventory = (InventoryManager)formatter.Deserialize(stream);
            stream.Close();
        }
    }
}
