using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInventory : MonoBehaviour // Activates Inventory UI with button press
{
    public GameObject InventoryScreen;
    public InventoryUI UI;
    public bool isInventoryActive = false;
    public GameObject Crosshair;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ActivateUI();
        }
    }

    public void ActivateUI()
    {
        isInventoryActive = !isInventoryActive;
        InventoryScreen.SetActive(isInventoryActive);

        if (isInventoryActive)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Crosshair.SetActive(false);
            Time.timeScale = 0f;
                
        }

        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Crosshair.SetActive(true);
            Time.timeScale = 1;
        }
            UI.UpdateUI(); // Get UI Changes
    }
}
