using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _promptText; // Text for interactable prompts
    InventoryManager inventory; // Player inventory

    private void Start()
    {
        inventory = InventoryManager.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            inventory.Save(inventory); // Saves inventory data
        }

        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            inventory.UpdateInventory(inventory.LoadData()); // Loads inventory data
        }
    }

    private void OnApplicationQuit()
    {
        //inventory.Save(inventory);
    }

    // Update is called once per frame
    public void UpdateText(string promptMessage)
    {
        _promptText.text = promptMessage;
    }
}
