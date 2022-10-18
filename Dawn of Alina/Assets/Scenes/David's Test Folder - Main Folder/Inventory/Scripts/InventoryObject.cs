using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory System/Inventory")]
[System.Serializable]
public class InventoryObject : ScriptableObject 
{
    InventoryManager Inventory;
    public string SavePath = "/inventory";

    private void Awake()
    {
        Inventory = InventoryManager.instance;
        Debug.Log(Inventory.ToString());
    }


    [ContextMenu("Save")]
    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, SavePath + ".json"),FileMode.Create,FileAccess.Write);
        formatter.Serialize(stream,Serializer.Serialize("Hello"));
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
