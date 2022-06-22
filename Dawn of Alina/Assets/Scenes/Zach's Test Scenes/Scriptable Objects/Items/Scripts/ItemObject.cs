using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines data regarding game items
public enum ItemType
{
    Potion,
    Equipment,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite UiDisplay;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;

    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
    }
}
