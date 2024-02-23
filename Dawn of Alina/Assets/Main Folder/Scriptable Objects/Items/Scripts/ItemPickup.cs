using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Collider ItemCollider;
    [SerializeField] private string pickupTag = "isPickupable";
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (ItemCollider.transform.CompareTag(pickupTag))
        { 
            InventoryManager.instance.AddItem(item, 1);
                Destroy(ItemCollider.gameObject);
        }
    }
}
