using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string[] itemNames = new string[InventoryManager.instance.ItemData.Count];
    public int[] itemAmounts = new int[InventoryManager.instance.ItemData.Count];
    public float playerHealth;
    public float[] playerPos;
   
    public SaveData ()
    {
        InventoryManager inventory = InventoryManager.instance;
        PlayerHealth PlayerData = PlayerHealth.instance;
        playerHealth = PlayerData.health;
        playerPos = new float[3];
        playerPos[0] = PlayerData.transform.position.x;
        playerPos[1] = PlayerData.transform.position.y;
        playerPos[2] = PlayerData.transform.position.z;

        foreach (KeyValuePair<Item, int> pair in inventory.ItemData)
        {
            for(int i = 0; i < inventory.ItemData.Count; i++)
            {
                itemAmounts[i] = pair.Value;
                itemNames[i] = pair.Key.Name;
            }
        }



    }
}
