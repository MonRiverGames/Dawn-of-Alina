using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public Sprite icon;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon = item.icon;
        transform.GetChild(0).GetComponent<Image>().sprite = icon;
      
    }

    public void ClearSlot()
    {
        item = null;
        icon = null;
        transform.GetChild(0).GetComponent<Image>().sprite = null;

    }

}
