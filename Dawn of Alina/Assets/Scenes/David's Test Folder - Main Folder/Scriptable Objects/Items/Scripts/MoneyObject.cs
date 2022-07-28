using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Money Item", menuName = "Inventory/Items/Money Item")]
public class MoneyObject : Item
{
    private void Awake()
    {
        type = ItemType.Money;
        stackLimit = 100000;
    }

}
