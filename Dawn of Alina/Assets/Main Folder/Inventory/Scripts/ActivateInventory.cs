using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInventory : MonoBehaviour // Activates Inventory UI with button press
{
    [SerializeField] private GameObject InventoryScreen; // reference to the Inventory Screen
    [SerializeField] private InventoryUI UI; // Reference to actual inventory UI and accompanying scripts
    [SerializeField] private bool isInventoryActive = false; // Boolean checks wether or not inventory is open
    [SerializeField] private GameObject Crosshair; // Reference to crosshair in center of screen

    public void Update() 
    {
        if (Input.GetKeyDown(KeyCode.I)) // If user presses I Key, then inventory will open
        {
            ActivateUI();
        }
    }

    public void ActivateUI()
    {
        isInventoryActive = !isInventoryActive;
        InventoryScreen.SetActive(isInventoryActive);

        if (isInventoryActive) // If inventory is open
        {
            Cursor.lockState = CursorLockMode.Confined; 
            Crosshair.SetActive(false);
        }

        else // if inventory closed
        {
            Cursor.lockState = CursorLockMode.Locked; 
            Crosshair.SetActive(true);
        }
    }
}
