using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamSwitch : MonoBehaviour
{
    public GameObject FirstCam; // First-person camera
    public GameObject ThirdCam; // Third-person camera
    bool isThirdPerson = false; // Boolean specifies either 1st or 3rd Person

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // Switches cam mode on letter press
        {
            isThirdPerson = !isThirdPerson;
            StartCoroutine(CameraChange()); // Change Camera
        }
    }

    IEnumerator CameraChange() // Sets the active camera and enables appropriate audio listener
    {
        yield return new WaitForSeconds(0.01f);
        FirstCam.SetActive(!isThirdPerson);
        ThirdCam.SetActive(isThirdPerson);
        FirstCam.GetComponent<AudioListener>().enabled = !isThirdPerson; 
        ThirdCam.GetComponent<AudioListener>().enabled = isThirdPerson;
        yield return null;
    }
}
