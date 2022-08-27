using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : MonoBehaviour
{
    public GameObject greatSword;

    public void EnableGS()
    {
        greatSword.GetComponent<BoxCollider>().enabled = true;
    }

    public void DisableGS()
    {
        greatSword.GetComponent<BoxCollider>().enabled = false;
    }
}
