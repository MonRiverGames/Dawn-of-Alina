using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform InventoryParent;
    public Transform HotbarParent;
    InventoryManager inventory;
    InventorySlot[] slots;
    InventorySlot[] hotbarSlots;
    int totalLength;

    // Start is called before the first frame update
    void Start()
    {
        inventory = InventoryManager.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = InventoryParent.GetComponentsInChildren<InventorySlot>();
        hotbarSlots = HotbarParent.GetComponentsInChildren<InventorySlot>();
        totalLength = slots.Length + hotbarSlots.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        for(int i = 0; i < totalLength; i++)
        {
            if(i < inventory.items.Count && i < inventory.HotbarSpace)
            {
                hotbarSlots[i].AddItem(inventory.items[i]);
            }

            if(i < inventory.items.Count && i >= inventory.HotbarSpace)
            {
                slots[i].AddItem(inventory.items[i]);
            }
        }
    }
}
