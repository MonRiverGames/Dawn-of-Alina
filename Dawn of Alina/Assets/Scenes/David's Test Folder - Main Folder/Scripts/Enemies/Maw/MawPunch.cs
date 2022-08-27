using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MawPunch : MonoBehaviour
{
    public GameObject fist;

    public void EnableFist()
    {
        fist.GetComponent<BoxCollider>().enabled = true;
    }

    public void DisableFist()
    {
        fist.GetComponent<BoxCollider>().enabled = false;
    }
}
