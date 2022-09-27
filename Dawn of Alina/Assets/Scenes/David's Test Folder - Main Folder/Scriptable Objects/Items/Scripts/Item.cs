using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Equipment,
    Potion,
    Magic,
    Money,
    Plant,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/Default Item")]
public class Item : ScriptableObject // Holds data for Item
{
    public string Name = "New Item";
    public Sprite icon = null;
    public GameObject ItemPrefab;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    public int itemValue;
    public int stackLimit;
    public bool inSlot;

    private void Awake()
    {
        type = ItemType.Equipment;
    }
}

