using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public float xRot = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    
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
}
