using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AdvancedCharacterController))]
[RequireComponent(typeof(Controllable))]
public class AdvancedPlayerController : MonoBehaviour
{
    private AdvancedCharacterController controller;
    private Controllable controllable;
    private Camera playerCamera;

    [SerializeField]
    private float mouseSensitivity = 5f;

    private float pitchRotation = 0f;
    private Vector2 inputMove;
    private Vector2 inputLook;
    private bool jumped = false;

    void Start()
    {
        this.controller = GetComponent<AdvancedCharacterController>();

        this.playerCamera = GetComponentInChildren<Camera>();

        this.controllable = GetComponent<Controllable>();

        this.controllable.AddActionMap("Player");
        this.controllable.AddAction("Player", "Move", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnMove);
        this.controllable.AddAction("Player", "Look", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnLook);
        this.controllable.AddAction("Player", "Jump", ActionTypeHandler.ActionType.Performed, OnJump);
    }

    void Update()
    {
        HandleCameraMovement();
        MoveController();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        this.inputLook = context.ReadValue<Vector2>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        this.inputMove = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        this.jumped = true;
    }

    void HandleCameraMovement()
    {
        float mouseX = this.inputLook.x * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = this.inputLook.y * mouseSensitivity * Time.fixedDeltaTime;

        this.transform.Rotate(Vector3.up * mouseX);

        pitchRotation -= mouseY;
        pitchRotation = Mathf.Clamp(pitchRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(pitchRotation, 0, 0);
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
