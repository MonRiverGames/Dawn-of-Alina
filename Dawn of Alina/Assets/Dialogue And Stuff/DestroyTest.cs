using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTest : MonoBehaviour
{
    [SerializeField]
    GameObject goodbyeObject;

    [SerializeField]
    GameObject helloObject;

    void Start()
    {
        helloObject.SetActive(false);
    }

    public void DestroyGameObject()
    {
        Destroy(goodbyeObject);
        helloObject.SetActive(true);
    }
}
