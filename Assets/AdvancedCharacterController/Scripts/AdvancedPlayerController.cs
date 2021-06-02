using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(IControllable), typeof(AdvancedCharacterController), typeof(AdvancedPlayerCameraController))]
public class AdvancedPlayerController : MonoBehaviour
{
    private IControllable controllable;
    private AdvancedCharacterController controller;
    private AdvancedPlayerCameraController cameraController;

    void Start()
    {
        this.controller = GetComponent<AdvancedCharacterController>();
        this.cameraController = GetComponent<AdvancedPlayerCameraController>();

        this.controllable = GetComponent<IControllable>();

        this.controllable.AddActionMap("Player");
        this.controllable.AddAction("Player", "Move", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnMove);
        this.controllable.AddAction("Player", "Look", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnLook);
        this.controllable.AddAction("Player", "Jump", ActionTypeHandler.ActionType.Performed, OnJump);
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
}
