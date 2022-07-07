using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public GameObject camPos;
    public Vector3 offSet;
    public GameObject head;
    
    
    public GameObject InventoryScreen;
    bool isInventoryActive = false;
    public float xRot = 0f;
    public float yRot = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
   
    void Start()
    {
        camPos.transform.position = head.transform.position + offSet;
    }

    public void Update()
    {
        camPos.transform.position = head.transform.position + offSet;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //Calculate camera rotation for looking up and down
        xRot -= (mouseY * Time.deltaTime) * ySensitivity;
        xRot = Mathf.Clamp(xRot, -80f, 80f);

        yRot += (mouseX * Time.deltaTime) * xSensitivity;
        
        //Apply to cam transform
        cam.transform.localRotation = Quaternion.Euler(xRot, yRot, 0f);
        //rot player left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    // Added Cam Switching for new input system  
    public void ActivateUI()
    {
        print("Inventory");
        isInventoryActive = !isInventoryActive;
        InventoryScreen.SetActive(isInventoryActive);
    }
}