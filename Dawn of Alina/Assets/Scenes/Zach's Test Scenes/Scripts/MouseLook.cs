using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour // Handles looking with mouse 
{
    public float mouseSensitivity = 100f; // Sensitivity 
    public Transform playerBody;
    public float xRotation = 0f; 

    // Start is called before the first frame update
   /* void Start()
    {
        // Locks Cursor during player movement
        Cursor.lockState = CursorLockMode.Locked;
    }*/

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Gets input from mouse in X direction
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Gets input from mouse in Y direction

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Sets bounds for Xrotation to 180 degrees

        // Roatates Player   
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX); 
    }
}
