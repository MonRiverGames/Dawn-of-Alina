using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteract : Interactable
{
    public GameObject ShopUI;
    public bool isShopActive = true;
    public GameObject Crosshair;

    protected override void Interact() // Opens Shop upon interaction
    {
        Debug.Log("Shop");
        isShopActive = !isShopActive;
        ShopUI.SetActive(isShopActive);

        if (isShopActive)
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
    }
}
