using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    
    private Vector3 playerVelocity;
    
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float crouchTimer;

    public bool isGrounded;
    public bool lerpCrouch;
    public bool crouching;
    public bool sprinting;
    
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1f, p);
                
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2f, p);
                
            }

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    
    //Recieve input from InputManager.cs and apply to character controller
    public void ProcessMove(Vector3 input)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = input.x;
        moveDir.y = input.y;
        moveDir.z = input.z;
        controller.Move(transform.TransformDirection(moveDir) * speed * Time.deltaTime);
        
        playerVelocity.y -= gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * 3f * gravity);
        }
    }
    
    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }
    
    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = 8f;
        }
        else
        {
            speed = 5f;
        }
    }

}
