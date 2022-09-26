using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public Transform ShopParent; // Main Inventory
    InventoryManager inventory; // Inventory instance
    ShopManager shop; // Shop instance
    InventorySlot[] slots;
    ShopItem[] shopItems;
    public GameObject Player;
    PlayerLook playerLook;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
