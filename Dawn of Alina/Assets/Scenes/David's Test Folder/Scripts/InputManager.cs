using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    private PlayerMotor playerMotor;
    private PlayerLook playerLook;
    
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        
        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();
        
        onFoot.Jump.performed += ctx => playerMotor.Jump();
        onFoot.Crouch.performed += ctx => playerMotor.Crouch();
        onFoot.Sprint.performed += ctx => playerMotor.Sprint();
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        //Tell playermotor to move using value from movement action
        playerMotor.ProcessMove(onFoot.Movement.ReadValue<Vector3>());
    }

    private void LateUpdate()
    {
        playerLook.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
