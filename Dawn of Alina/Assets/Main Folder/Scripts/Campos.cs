using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campos : MonoBehaviour
{
    public GameObject player;
    public Vector3 offSet;

    public float sensX;
    public float sensY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");


        transform.position = player.transform.position + offSet;
        transform.rotation = player.transform.rotation;
    }
}
