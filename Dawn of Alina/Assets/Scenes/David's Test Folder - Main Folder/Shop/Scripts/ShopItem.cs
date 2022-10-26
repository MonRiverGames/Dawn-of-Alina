using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public Item item; // Item in Shop slot
    public Sprite icon; // Item Icon for Shop
    public bool isViewing; // If player is clicking on given slot
    public Transform ItemInfo;
    public string ItemValue;
    public InventoryUI UI;

    // On mouse enter 
    public void EnterItem() => isViewing = true;

    // On mouse exit
    public void ExitItem() => isViewing = false;

    // Displays info and purchase panel
    public void DisplayInfo()
    {
        if (isViewing)
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
            ItemInfo.GetChild(4).GetComponent<Button>().enabled = true;
            ItemInfo.gameObject.SetActive(true);
        }
        else
        {
            ItemInfo.GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
            ItemInfo.GetChild(1).GetComponent<Image>().sprite = null;
            ItemInfo.GetChild(2).GetComponent<TextMeshProUGUI>().text = null;
            ItemInfo.GetChild(3).GetComponent<TextMeshProUGUI>().text = null;
            ItemInfo.GetChild(4).GetComponent<Button>().enabled = false;
            ItemInfo.gameObject.SetActive(false);
        }
    }

    public void BuyItem() // purchases item
    {
        ShopItem slot = this;
        InventoryManager.instance.AddItem(slot.item);
        InventoryManager.instance.GoldAmount -= slot.item.itemValue;
        UI.UpdateUI();
    }
}
