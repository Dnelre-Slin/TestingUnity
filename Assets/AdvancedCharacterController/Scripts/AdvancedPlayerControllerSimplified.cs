using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if (ENABLE_INPUT_SYSTEM)
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(AdvancedCharacterController), typeof(AdvancedPlayerCameraController))]
public class AdvancedPlayerControllerSimplified : MonoBehaviour
{
    #if (ENABLE_LEGACY_INPUT_MANAGER)
    [SerializeField]
    private string inputHorizontalAxis = "Horizontal";
    [SerializeField]
    private string inputVerticalAxis = "Vertical";
    [SerializeField]
    private string inputMouseXAxis = "Mouse X";
    [SerializeField]
    private string inputMouseYAxis = "Mouse Y";
    [SerializeField]
    private string inputJumpButton = "space";
    #elif (ENABLE_INPUT_SYSTEM)
    [SerializeField]
    private InputActionAsset actionAsset = null;
    [SerializeField]
    private string actionMap = "Player";
    [SerializeField]
    private string inputMove = "Move";
    [SerializeField]
    private string inputLook = "Look";
    [SerializeField]
    private string inputJump = "Jump";
    #endif

    private AdvancedCharacterController controller;
    private AdvancedPlayerCameraController cameraController;
    void Start()
    {
        this.controller = GetComponent<AdvancedCharacterController>();
        this.cameraController = GetComponent<AdvancedPlayerCameraController>();

        #if (ENABLE_INPUT_SYSTEM)
        InputActionMap inputActionMap = this.actionAsset.FindActionMap(this.actionMap);
        inputActionMap.Enable();
        InputAction inputActionMove = inputActionMap.FindAction(inputMove);
        inputActionMove.performed += ctx => OnMove(ctx);
        inputActionMove.canceled += ctx => OnMove(ctx);
        InputAction inputActionLook = inputActionMap.FindAction(inputLook);
        inputActionLook.performed += OnLook;
        inputActionLook.canceled += OnLook;
        InputAction inputActionJump = inputActionMap.FindAction(inputJump);
        inputActionJump.performed += OnJump;
        #endif
    }

    #if (ENABLE_LEGACY_INPUT_MANAGER)
    void Update()
    {
        if (this.inputHorizontalAxis != "" && this.inputVerticalAxis != "")
        {
            Vector2 playerMove = new Vector2(Input.GetAxisRaw(this.inputHorizontalAxis), Input.GetAxisRaw(this.inputVerticalAxis));
            this.controller.Move(playerMove);
        }
        if (this.inputMouseXAxis != "" && this.inputMouseYAxis != "")
        {
            Vector2 playerLook = new Vector2(Input.GetAxisRaw(this.inputMouseXAxis), Input.GetAxisRaw(this.inputMouseYAxis));
            this.cameraController.Look(playerLook);
        }
        if (this.inputJumpButton != "")
        {
            if (Input.GetKeyDown(this.inputJumpButton))
            {
                this.controller.Jump();
            }
        }
    }
    #endif

    #if (ENABLE_INPUT_SYSTEM)
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
    #endif
}
