using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    [TextArea(15, 20)]
    public string description;
}


