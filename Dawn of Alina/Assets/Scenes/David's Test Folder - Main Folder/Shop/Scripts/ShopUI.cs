using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public Transform ShopParent; // Main Inventory
    ShopManager shop; // Shop instance
    ShopItem[] slots;
    public GameObject Player;
    PlayerLook playerLook;

    void Start()
    {
        shop = ShopManager.instance;
        slots = ShopParent.GetComponentsInChildren<ShopItem>();
        shop.shopItemCount = slots.Length;
        playerLook = Player.GetComponent<PlayerLook>();
    }

    public bool inSlot(Item item) // Checks if item is present in inventory slot
    {
        foreach (ShopItem slot in slots)
        {
            if (slot.item = item)
            {
                return true;
            }
        }
        return false;
    }
}
