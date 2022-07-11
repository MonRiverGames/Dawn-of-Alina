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
    public int stackLimit;

    private void OnEnable() 
    {
        amount = 1;
    }
}

