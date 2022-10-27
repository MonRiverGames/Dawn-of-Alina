using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaws : MonoBehaviour
{
    [SerializeField]
    public GameObject jaws;

    public void EnableJaws()
    {
        jaws.GetComponent<BoxCollider>().enabled = true;
    }

    public void DisableJaws()
    {
        jaws.GetComponent<BoxCollider>().enabled = false;
    }
}