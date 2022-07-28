using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion Item", menuName = "Inventory/Items/Plant Item")]
public class PlantObject : Item 
{
    private void Awake()
    {
        type = ItemType.Plant;
        stackLimit = 64;
    }
}
