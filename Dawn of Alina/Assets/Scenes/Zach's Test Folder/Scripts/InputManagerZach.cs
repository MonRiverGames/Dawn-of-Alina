using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerZach : MonoBehaviour
{
    private ZachPlayerInput playerInput;
    public ZachPlayerInput.OnFootActions onFoot;
    public ZachPlayerInput.CameraControlActions cameraControl;
    private PlayerMotor playerMotor;
    private PlayerLookZach playerLook;

    void Awake()
    {
        playerInput = new ZachPlayerInput();
        onFoot = playerInput.OnFoot;
        cameraControl = playerInput.CameraControl;
        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLookZach>();
        onFoot.Jump.performed += ctx => playerMotor.Jump();
        cameraControl.SwitchCameraView.performed += ctx => playerLook.getCamChange();
        onFoot.OpenInventory.performed += ctx => playerLook.ActivateUI();
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
        cameraControl.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
        cameraControl.Disable();
    }
}
