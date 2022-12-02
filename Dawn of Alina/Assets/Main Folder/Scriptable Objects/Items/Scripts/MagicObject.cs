using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Magic Item", menuName = "Inventory/Items/Magic Item")]
public class MagicObject : Item
{
    public int manaCost;
    public int damage;

    private void Awake()
    {
        type = ItemType.Magic;

    }
}
