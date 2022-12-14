using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public GameObject InventoryScreen;
    public InventoryUI UI;
    public bool isInventoryActive = true;
    public float xRot = 0f;
    //public float yRot = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
   
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //Calculate camera rotation for looking up and down
        xRot -= (mouseY * Time.deltaTime) * ySensitivity;
        xRot = Mathf.Clamp(xRot, -80f, 80f);
        
        //Apply to cam transform
        cam.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        //rot player left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}