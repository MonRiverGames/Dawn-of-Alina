using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LHandSlot : MonoBehaviour
{
    public Item item;
    public Sprite icon;
    public InventoryUI UI;
    public Button RemoveButton;
    public bool isFilled;
    public bool isViewing;
    public Transform ItemInfo;
    public Transform LHand;
    public GameObject ItemPrefab;
    public string ItemValue;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma) && isFilled && isViewing)
        {
            UnequipLeft();
        }
    }
    public void AddItem(Item newItem) // Adds item to slot
    {
        item = newItem;

        if (newItem.type == ItemType.Equipment)
        {
            icon = item.icon;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetComponent<Image>().sprite = icon;
            isFilled = true;
            item.inSlot = true;
            ItemPrefab = item.ItemPrefab;
            UI.EnableRemoveButton();
            Instantiate(ItemPrefab, LHand);
        }
    }

    public void ClearSlot() // Clears a given slot
    {
        item = null;
        icon = null;
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        transform.GetChild(0).gameObject.SetActive(false);
        isFilled = false;
        UI.EnableRemoveButton();
    }

    public void OnRemoveButton()
    {
        InventoryManager.instance.UnequipLeft();
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
        }
        else
        {
            ItemInfo.GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
            ItemInfo.GetChild(1).GetComponent<Image>().sprite = null;
            ItemInfo.GetChild(2).GetComponent<TextMeshProUGUI>().text = null;
            ItemInfo.GetChild(3).GetComponent<TextMeshProUGUI>().text = null;
            ItemInfo.gameObject.SetActive(false);
        }
    }

    public void UnequipLeft()
    {
        if (isFilled && isViewing)
        {
            InventoryManager.instance.EquipLeftItem(item);
            print("Unequip Left");
            OnRemoveButton();
        }
    }

}
