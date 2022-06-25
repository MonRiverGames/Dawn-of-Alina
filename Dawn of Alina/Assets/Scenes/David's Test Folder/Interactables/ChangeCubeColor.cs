using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCubeColor : MonoBehaviour
{
    MeshRenderer mesh;
    public Color[] colors;
    private int colorIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
