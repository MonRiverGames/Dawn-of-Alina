using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _promptText;

    public InventoryObject inventory;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            inventory.Save();
        }

        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            inventory.Load();
        }
    }

    // Update is called once per frame
    public void UpdateText(string promptMessage)
    {
        _promptText.text = promptMessage;
    }
}
