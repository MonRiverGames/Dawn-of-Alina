using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupZach : MonoBehaviour
{
    public Collider ItemCollider;
    [SerializeField] private string pickupTag = "isPickupable";
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (ItemCollider.transform.CompareTag(pickupTag))
        { 
            InventoryManagerZach.instance.AddItem(item);
                Destroy(ItemCollider.gameObject);
        }
    }
}
