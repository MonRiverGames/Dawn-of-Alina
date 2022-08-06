using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInventory : MonoBehaviour
{
    public GameObject InventoryScreen;
    public InventoryUI UI;
    public bool isInventoryActive = true;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ActivateUI();
        }
    }

    public void ActivateUI()
    {
            Debug.Log("Inventory");
            isInventoryActive = !isInventoryActive;
            InventoryScreen.SetActive(isInventoryActive);

            if (isInventoryActive)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }

            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            UI.UpdateUI();
    }
}
