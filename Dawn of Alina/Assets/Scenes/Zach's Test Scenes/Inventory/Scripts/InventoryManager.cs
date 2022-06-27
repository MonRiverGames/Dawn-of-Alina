using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
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
    #endregion

    public List<Item> items = new List<Item>();
    public int InventorySpace = 29;
    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallback;

    public bool AddItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= InventorySpace)
            {
                Debug.Log("Inventory Full");
                return false;
            }
            items.Add(item);
            
            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

    }
}
