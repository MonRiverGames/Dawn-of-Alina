using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<Item> items = new List<Item>(); // Holds items; basically the inventory
    public List<Item> EquippedItems = new List<Item>();
    public int InventorySpace = 25;
    public InventoryUI UI;

    public void Start()
    {
        Item Default = ScriptableObject.CreateInstance<Item>();
        Default.type = ItemType.Default;
        EquippedItems = new List<Item>{Default, Default};
    }

    public void AddItem(Item item)
    {
        if (!items.Contains(item)) // If Item is not in inventory
        {
            if (items.Count >= InventorySpace)
            {
                Debug.Log("Inventory Full");
                return;
            }
            items.Add(item);
        }

        else // Item is already in inventory
        {
            item.amount++; // Increase Item amount
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
                print("WE OUT HERE IN INVENTORY MANAGER");
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
        items.Remove(item);
    }
}