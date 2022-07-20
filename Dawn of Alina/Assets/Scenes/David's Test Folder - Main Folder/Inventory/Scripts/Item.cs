using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject // Holds data for Item
{
    public string Name = "New Item";
    public Sprite icon = null;
    [TextArea(15, 20)]
    public string description;
    public int amount;
    public int itemValue;
    public int stackLimit;
    public bool inSlot;

    private void OnEnable() // Sets amount of item to 1 as default
    {
        amount = 1;
    }
}

