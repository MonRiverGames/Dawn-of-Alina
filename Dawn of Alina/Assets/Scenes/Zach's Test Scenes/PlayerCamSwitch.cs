using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamSwitch : MonoBehaviour
{
    public GameObject FirstCam; // First-person camera
    public GameObject ThirdCam; // Third-person camera
    public int camMode; // Specifies either 1st or 3rd Person

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // Switches cam mode on letter press
        {
            if (camMode == 1)
            {
                camMode = 0;
            }

            else
            {
                camMode += 1;
            }

            StartCoroutine(CamChange()); // Change Camera
        }
    }

    IEnumerator CamChange() // Sets the active camera and enables appropriate audio listener
    {
        yield return new WaitForSeconds(0.01f);

        if(camMode == 0) // First Person
        {
            FirstCam.SetActive(true);
            ThirdCam.SetActive(false);
            FirstCam.GetComponent<AudioListener>().enabled = true;
            ThirdCam.GetComponent<AudioListener>().enabled = false;
        }

        if (camMode == 1) // Third Person
        {
            ThirdCam.SetActive(true);
            FirstCam.SetActive(false);
            FirstCam.GetComponent<AudioListener>().enabled = false;
            ThirdCam.GetComponent<AudioListener>().enabled = true;
        }
    }
}
