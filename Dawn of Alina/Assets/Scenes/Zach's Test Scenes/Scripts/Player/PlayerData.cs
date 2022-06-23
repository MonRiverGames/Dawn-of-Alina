using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public InventoryObject inventory;

    public void OnTriggerEnter(Collider other)
    {
        GroundItem item = other.GetComponent<GroundItem>();

        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }

    public void Start()
    {
        inventory.Load();
        Debug.Log("Inventory Loaded");
    }

    private void OnApplicationQuit()
    {
        inventory.Save();
        Debug.Log("Inventory Saved");
    }
}
