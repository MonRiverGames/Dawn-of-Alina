using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float rayDis = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Clears the interactable message prompt when not looking at a game object.
        playerUI.UpdateText(string.Empty);
        
        //Creates a ray shooting out of the cams pos shooting forward
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        
        // Store collision
        RaycastHit hitInfo; 
        
        //Draws the ray so we can see it
        Debug.DrawRay(ray.origin, ray.direction * rayDis, Color.red);

        if (Physics.Raycast(ray, out hitInfo, rayDis, mask))
        {
            //If the ray hits something
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
