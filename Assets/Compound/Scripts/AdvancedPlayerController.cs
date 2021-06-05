using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Controllable), typeof(AdvancedCharacterController), typeof(AdvancedPlayerCameraController))]
[RequireComponent(typeof(BaseInstigator))]
public class AdvancedPlayerController : MonoBehaviour
{
    private Controllable controllable;
    private AdvancedCharacterController controller;
    private AdvancedPlayerCameraController cameraController;
    private BaseInstigator instigator;

    void Start()
    {
        this.controller = GetComponent<AdvancedCharacterController>();
        this.cameraController = GetComponent<AdvancedPlayerCameraController>();
        this.instigator = GetComponent<BaseInstigator>();

        this.controllable = GetComponent<Controllable>();

        this.controllable.AddActionMap("Player");
        this.controllable.AddAction("Player", "Move", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnMove);
        this.controllable.AddAction("Player", "Look", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnLook);
        this.controllable.AddAction("Player", "Jump", ActionTypeHandler.ActionType.Performed, OnJump);
        this.controllable.AddAction("Player", "Interact", ActionTypeHandler.ActionType.Performed, OnInteract);
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
