using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamSwitch : MonoBehaviour
{
    public GameObject FirstCam; // First-person camera
    public GameObject ThirdCam; // Third-person camera
    public int camMode; // Either 1st or 3rd Person

    // Update is called once per frame
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

            StartCoroutine(CamChange());
        }
    }

    IEnumerator CamChange() // Sets the active camera and enables appropriate audio listener
    {
        yield return new WaitForSeconds(0.01f);

        if(camMode == 0)
        {
            FirstCam.SetActive(true);
            ThirdCam.SetActive(false);
            FirstCam.GetComponent<AudioListener>().enabled = true;
            ThirdCam.GetComponent<AudioListener>().enabled = false;
        }

        if (camMode == 1)
        {
            ThirdCam.SetActive(true);
            FirstCam.SetActive(false);
            FirstCam.GetComponent<AudioListener>().enabled = false;
            ThirdCam.GetComponent<AudioListener>().enabled = true;
        }
    }
}
