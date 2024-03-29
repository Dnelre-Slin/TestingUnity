﻿#if (ENABLE_INPUT_SYSTEM)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BaseInstigator), typeof(AdvancedCharacterController), typeof(AdvancedPlayerCameraController))]
[RequireComponent(typeof(InputSystemControlScheme))]
public class AdvancedPlayerController : MonoBehaviour
{
    private InputSystemControlScheme controlScheme;
    private AdvancedCharacterController controller;
    private AdvancedPlayerCameraController cameraController;
    private BaseInstigator instigator;

    void Start()
    {
        this.controller = GetComponent<AdvancedCharacterController>();
        this.cameraController = GetComponent<AdvancedPlayerCameraController>();
        this.instigator = GetComponent<BaseInstigator>();

        this.controlScheme = GetComponent<InputSystemControlScheme>();

        this.controlScheme.AddActionMap("Player");
        this.controlScheme.AddAction("Player", "Move", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnMove);
        this.controlScheme.AddAction("Player", "Look", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnLook);
        this.controlScheme.AddAction("Player", "Jump", ActionTypeHandler.ActionType.Performed, OnJump);
        this.controlScheme.AddAction("Player", "Interact", ActionTypeHandler.ActionType.Performed, OnInteract);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        this.cameraController.Look(context.ReadValue<Vector2>());
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        this.controller.Move(context.ReadValue<Vector2>());
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        this.controller.Jump();
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        this.instigator.Instigate();
    }
}
#endif