using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<ItemSlot> slots = new List<ItemSlot>();
    public bool isMoving;
    public ItemSlot grabbedItem;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        for(int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).GetComponent<ItemSlot>());
        }
        foreach (ItemSlot slot in slots)
            slot.LoadItem();
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
