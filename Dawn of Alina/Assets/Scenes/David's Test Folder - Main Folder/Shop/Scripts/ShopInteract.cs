using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteract : Interactable
{
    public GameObject ShopUI;
    public bool isShopActive = true; // if the ShopUI is sisplaying
    public GameObject Crosshair;

    protected override void Interact() // Opens Shop upon interaction
    {
        isShopActive = !isShopActive;
        ShopUI.SetActive(isShopActive);

        if (isShopActive) // If shop is displaying
        {
            Cursor.lockState = CursorLockMode.Confined; // Get rid of cursor
            Crosshair.SetActive(false); // Don't Display Crosshair
            Time.timeScale = 0f; // Pause Game
        }

        else // if shop is not displating
        {
            Cursor.lockState = CursorLockMode.Locked;
            Crosshair.SetActive(true);
            Time.timeScale = 1;
        }
    }
}
