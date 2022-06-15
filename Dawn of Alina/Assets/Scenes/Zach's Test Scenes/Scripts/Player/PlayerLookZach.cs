using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookZach : MonoBehaviour
{
    public Camera cam;
    public GameObject FirstCam;
    public GameObject ThirdCam;
    public float xRot = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    bool isThirdPerson = false; // Boolean specifies either 1st or 3rd Person
    void Start()
    {
        // Locks Cursor during player movement
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //Calculate camera rotation for looking up and down
        xRot -= (mouseY * Time.deltaTime) * ySensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        //Apply to cam transform
        cam.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        //rot player left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    // Added Cam Switching for new input system
    public void getCamChange()
    {
        print("Camera Change");
        isThirdPerson = !isThirdPerson;
        FirstCam.SetActive(!isThirdPerson);
        ThirdCam.SetActive(isThirdPerson);
        FirstCam.GetComponent<AudioListener>().enabled = !isThirdPerson;
        ThirdCam.GetComponent<AudioListener>().enabled = isThirdPerson;
    }
}