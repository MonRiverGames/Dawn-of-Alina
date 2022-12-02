using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion Item", menuName = "Inventory/Items/Potion Item")]
public class PotionObject : Item 
{
    private void Awake()
    {
        type = ItemType.Potion;
        stackLimit = 64;
    }
}
