using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public GameObject sword;

    public void EnableSword()
    {
        sword.GetComponent<BoxCollider>().enabled = true;
    }

    public void DisableSword()
    {
        sword.GetComponent<BoxCollider>().enabled = false;
    }

}