using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour // Manages the info for each inventory slot
{
    public Item item;
    public InventoryUI UI;
    public Transform ItemInfo; // Item info panel
    public string ItemValue;
    public bool isFilled; // If there is an item present in slot
    public int AmountInSlot;

    [SerializeField] private ShopInteract shop;
    [SerializeField] Button SellButton;
    [SerializeField] Sprite icon;
    [SerializeField] Button RemoveButton;
    [SerializeField] bool isViewing; // If player is looking at slot in inventory

    public void AddItem(Item newItem, int amount) // Adds item to slot
    {
        AmountInSlot = amount;
        this.item = newItem;
        icon = item.icon;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<Image>().sprite = icon;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = AmountInSlot.ToString();
        SellButton.onClick.RemoveAllListeners();
        SellButton.onClick.AddListener(SellItem);
        isFilled = true;
        item.inSlot = true;
        UI.EnableRemoveButton();
        this.item = newItem;

        if(AmountInSlot == 1)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false; // doesnt Display the amount
        }

        if (AmountInSlot > 1)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = true; // Displays the amount
        }
        item = newItem;
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
        UI.EnableRemoveButton();
    }

    public void OnRemoveButton()
    {
        if (this.item != null)
        {
            this.AmountInSlot--;
            InventoryManager.instance.DecrementItem(this.item,1);

            if (AmountInSlot == 0)
            {
                string prevName = this.item.name;
                ClearSlot();
                Debug.Log("Item: " + prevName + " Amount: 0");
                UI.UpdateUI();
                return;
            }
            UI.UpdateUI();
            Debug.Log("Item: " + this.item.name + " Amount: " + InventoryManager.instance.ItemData[this.item]);
        }
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
            InventoryManager.instance.DecrementItem(item,1);
            InventoryManager.instance.GoldAmount += item.itemValue;
            UI.UpdateUI();
        }
        else
        {
            OnRemoveButton();
            ClearSlot();
            UI.UpdateUI();
        }
    }
}