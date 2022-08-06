using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory/Items/Equipment Item")]
public class EquipmentObject : Item
{
    public float protectionBonus;
    public int durability;
    public bool equipped;

    private void Awake()
    {
        type = ItemType.Equipment;
        stackLimit = 1;
    }
}
