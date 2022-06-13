using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables for player movement
    public float speed = 12f; // Speed of player
    public float gravity = -9.81f; // Gravity
    public float jumpHeight = 2f; // Height of Jump
    Vector3 velocity; // velocity vector
    
    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.2f; // Distance from ground
    public LayerMask groundmask; // Layer for ground
    bool isGrounded; // Check if player is on the ground

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask); // Checks if ground object is within range of player

        if (isGrounded && velocity.y < 0) // removes velocity if grounded
        {
            velocity.y = -2f; 
        }

        float x = Input.GetAxis("Horizontal"); // Movement Axes
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z; // chooses move vector from where player is facing
        controller.Move(move * speed * Time.deltaTime); // Moves player

        // Player Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // Jumps player
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculates height of jump
        }

        velocity.y += gravity * Time.deltaTime; // Sets velocity in Y direction using gravity (falling)
        controller.Move(velocity * Time.deltaTime); 
    }
}