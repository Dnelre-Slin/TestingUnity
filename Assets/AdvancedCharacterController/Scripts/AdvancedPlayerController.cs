using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// [RequireComponent(typeof(AdvancedCharacterController))]
[RequireComponent(typeof(Controllable), typeof(AdvancedCharacterController), typeof(AdvancedPlayerCameraController))]
public class AdvancedPlayerController : MonoBehaviour
{
    private Controllable controllable;
    private AdvancedCharacterController controller;
    private AdvancedPlayerCameraController cameraController;
    private Vector2 inputMove;
    private bool jumped = false;

    void Start()
    {
        this.controller = GetComponent<AdvancedCharacterController>();
        this.cameraController = GetComponent<AdvancedPlayerCameraController>();

        this.controllable = GetComponent<Controllable>();

        this.controllable.AddActionMap("Player");
        this.controllable.AddAction("Player", "Move", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnMove);
        this.controllable.AddAction("Player", "Look", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnLook);
        this.controllable.AddAction("Player", "Jump", ActionTypeHandler.ActionType.Performed, OnJump);
    }

    void Update()
    {
        MoveController();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        this.cameraController.Look(context.ReadValue<Vector2>());
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        this.inputMove = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        this.jumped = true;
    }

    void MoveController()
    {
        if (this.jumped)
        {
            this.controller.Jump();
            this.jumped = false;
        }
        this.controller.Move(this.inputMove);
    }
}
