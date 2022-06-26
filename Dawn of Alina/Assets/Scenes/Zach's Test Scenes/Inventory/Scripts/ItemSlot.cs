using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemObject item;
    public bool isFree;

    public bool isViewing;

    public void LoadItem()
    {
        if (item != null)
        {
            isFree = false;
            Image itemSprite = transform.GetChild(0).GetComponent<Image>();
            itemSprite.enabled = true;
            itemSprite.sprite = item.sprite;
        }
        else
        {
            isFree = true;
        }
    }

    void ClearItem()
    {
        Image itemSprite = transform.GetChild(0).GetComponent<Image>();
        itemSprite.sprite = null;
        itemSprite.enabled = false;
        isFree = true;
    }

    // Changes the current item that is being looked at. On mouse enter
    public void EnterSlot() => isViewing = true;

    // On mouse exit
    public void ExitSlot() => isViewing = false;

    // Run on click and if inside box
    public void ClickItem()
    {

    }

    public void StartDrag()
    {
        if (!InventoryManager.instance.isMoving && !isFree && isViewing)
        {
            InventoryManager.instance.isMoving = true;
            InventoryManager.instance.grabbedItem = gameObject.GetComponent<ItemSlot>();
        }
    }

    public void EndDrag()
    {
        if (InventoryManager.instance.isMoving)
        {
            // If slot we are placing into is not empty
            if (!isFree)
            {
                // Save info
                ItemObject temp = item;
                item = InventoryManager.instance.grabbedItem.item;
                InventoryManager.instance.grabbedItem.item = temp;

                // Loads item info
                LoadItem();
                InventoryManager.instance.grabbedItem.LoadItem();

                InventoryManager.instance.grabbedItem = null;
                InventoryManager.instance.isMoving = false;
            }
            else
            {
                item = InventoryManager.instance.grabbedItem.item;

                // Loads item info
                LoadItem();
                InventoryManager.instance.grabbedItem.ClearItem();
                InventoryManager.instance.grabbedItem = null;
                InventoryManager.instance.isMoving = false;
            }
        }
    }
}
