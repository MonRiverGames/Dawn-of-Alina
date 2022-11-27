using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour // Manages the info for each inventory slot
{
    public Item item; 
    public Sprite icon;
    public Button RemoveButton;
    public InventoryUI UI;
    public bool isFilled; // If there is an item present in slot
    public bool isViewing; // If player is looking at slot in inventory
    public Transform ItemInfo; // Item info panel
    public string ItemValue;
    public ShopInteract shop;
    public Button SellButton;

    public void AddItem(Item newItem) // Adds item to slot
    {
        item = newItem;

        if (InventoryManager.instance.ItemData[item] <= newItem.stackLimit)
        {
            icon = item.icon;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetComponent<Image>().sprite = icon;
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = InventoryManager.instance.ItemData[item].ToString();
            SellButton.onClick.RemoveAllListeners();
            SellButton.onClick.AddListener(SellItem);
            isFilled = true;
            item.inSlot = true;
            UI.EnableRemoveButton();
            item = newItem;
        }
        else 
        {
            newItem.inSlot = true;
            InventoryManager.instance.AddItem(newItem);
            item = newItem;
        }

        if (InventoryManager.instance.ItemData[item] > 1)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = true; // Displays the amount
        }
    }

    public void ClearSlot() // Clears a given slot
    {
        item = null;
        icon = null;
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = null;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false;
        isFilled = false;
        UI.UpdateUI();
        UI.EnableRemoveButton();
    }

    public void OnRemoveButton()
    {
        InventoryManager.instance.Remove(item);
        ClearSlot();
    }

    // On mouse enter 
    public void EnterItem() => isViewing = true;

    // On mouse exit
    public void ExitItem() => isViewing = false;

    public void DisplayInfo()
    {
        if (isViewing && isFilled)
        {
            ItemInfo.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.name;
            ItemInfo.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
            ItemInfo.GetChild(1).GetComponent<Image>().sprite = item.icon;
            ItemInfo.GetChild(1).GetComponent<Image>().enabled = true;
            ItemInfo.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.description;
            ItemInfo.GetChild(2).GetComponent<TextMeshProUGUI>().enabled = true;
            ItemValue = "Value: " + item.itemValue.ToString();
            ItemInfo.GetChild(3).GetComponent<TextMeshProUGUI>().text = ItemValue;
            ItemInfo.GetChild(2).GetComponent<TextMeshProUGUI>().enabled = true;
            ItemInfo.gameObject.SetActive(true);

            if (shop.isShopActive)
            {
                ItemInfo.GetChild(4).gameObject.SetActive(true);
            }

            else
            {
                ItemInfo.GetChild(4).gameObject.SetActive(false);
            }
        }

        else
        {
            ItemInfo.GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
            ItemInfo.GetChild(1).GetComponent<Image>().sprite = null;
            ItemInfo.GetChild(2).GetComponent<TextMeshProUGUI>().text = null;
            ItemInfo.GetChild(3).GetComponent<TextMeshProUGUI>().text = null;
            ItemInfo.GetChild(4).gameObject.SetActive(false);
            ItemInfo.gameObject.SetActive(false);
        }
    }

    public void SellItem()
    {
        if (InventoryManager.instance.ItemData[item] > 0)
        {
            InventoryManager.instance.ItemData[item]--;
            InventoryManager.instance.GoldAmount += item.itemValue;
            UI.UpdateUI();
        }

        else
        {
            ClearSlot();
            UI.UpdateUI();
        }
    }

    public bool FirstItem()
    {
        return firstItem;
    }
}