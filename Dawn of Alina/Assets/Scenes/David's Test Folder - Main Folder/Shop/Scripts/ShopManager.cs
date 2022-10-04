using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region Singleton
    public static ShopManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion // InventoryManager instance

    public List<Item> shopItems = new List<Item>(); // Holds items for the shop
    public int shopItemCount = 25;
    public ShopUI UI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(Item item)
    {
        if (!shopItems.Contains(item)) // If Item is not in inventory
        {
            if (shopItems.Count >= shopItemCount)
            {
                Debug.Log("Inventory Full");
                return;
            }
            shopItems.Add(item);
        }
    }
}
