using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if (ENABLE_INPUT_SYSTEM)
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(Spaceship))]
public class SpaceshipControllerSimplified : MonoBehaviour
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
    private string inputThrustUp = "space";
    [SerializeField]
    private string inputThrustDown = "ctrl";
    [SerializeField]
    private string inputRollRight = "e";
    [SerializeField]
    private string inputRollLeft = "q";
    #elif (ENABLE_INPUT_SYSTEM)
    [SerializeField]
    private InputActionAsset actionAsset = null;
    [SerializeField]
    private string actionMap = "Spaceship";
    [SerializeField]
    private string inputThrustForward = "ThrustForward";
    [SerializeField]
    private string inputThrustRight = "ThrustRight";
    [SerializeField]
    private string inputThrustUp = "ThrustUp";
    [SerializeField]
    private string inputPitchYaw = "TurnPitchYaw";
    [SerializeField]
    private string inputRoll = "TurnRoll";
    #endif

    private Spaceship spaceship;

    void Start()
    {
        this.spaceship = GetComponent<Spaceship>();

        #if (ENABLE_INPUT_SYSTEM)
        InputActionMap inputActionMap = this.actionAsset.FindActionMap(this.actionMap);
        inputActionMap.Enable();
        InputAction inputActionThrustForward = inputActionMap.FindAction(this.inputThrustForward);
        inputActionThrustForward.performed += ctx => this.spaceship.OnThrustForward(ctx.ReadValue<float>());
        inputActionThrustForward.canceled += ctx => this.spaceship.OnThrustForward(ctx.ReadValue<float>());
        InputAction inputActionThrustRight = inputActionMap.FindAction(this.inputThrustRight);
        inputActionThrustRight.performed += ctx => this.spaceship.OnThrustRight(ctx.ReadValue<float>());
        inputActionThrustRight.canceled += ctx => this.spaceship.OnThrustRight(ctx.ReadValue<float>());
        InputAction inputActionThrustUp = inputActionMap.FindAction(this.inputThrustUp);
        inputActionThrustUp.performed += ctx => this.spaceship.OnThrustUp(ctx.ReadValue<float>());
        inputActionThrustUp.canceled += ctx => this.spaceship.OnThrustUp(ctx.ReadValue<float>());
        InputAction inputActionTurnPitchYaw = inputActionMap.FindAction(inputPitchYaw);
        inputActionTurnPitchYaw.performed += ctx => this.spaceship.OnTurnPitchYaw(ctx.ReadValue<Vector2>());
        inputActionTurnPitchYaw.canceled += ctx => this.spaceship.OnTurnPitchYaw(ctx.ReadValue<Vector2>());
        InputAction inputActionTurnRoll = inputActionMap.FindAction(inputRoll);
        inputActionTurnRoll.performed += ctx => this.spaceship.OnTurnRoll(ctx.ReadValue<float>());
        inputActionTurnRoll.canceled += ctx => this.spaceship.OnTurnRoll(ctx.ReadValue<float>());
        #endif
    }

    #if (ENABLE_LEGACY_INPUT_MANAGER)
    void Update()
    {
        if (this.inputHorizontalAxis != "" && this.inputVerticalAxis != "")
        {
            Vector2 thrustForwardRight = new Vector2(Input.GetAxisRaw(this.inputHorizontalAxis), Input.GetAxisRaw(this.inputVerticalAxis));
            this.spaceship.OnThrustForward(thrustForwardRight.x);
            this.spaceship.OnThrustRight(thrustForwardRight.y);
        }
        if (this.inputMouseXAxis != "" && this.inputMouseYAxis != "")
        {
            Vector2 turnPitchYaw = new Vector2(Input.GetAxisRaw(this.inputMouseXAxis), Input.GetAxisRaw(this.inputMouseYAxis));
            this.spaceship.OnTurnPitchYaw(turnPitchYaw);
        }
        if (this.inputThrustUp != "")
        {
            if (Input.GetKeyDown(this.inputThrustUp))
            {
                this.spaceship.OnThrustUp(1.0f);
            }
        }
        if (this.inputThrustDown != "")
        {
            if (Input.GetKeyDown(this.inputThrustDown))
            {
                this.spaceship.OnThrustUp(-1.0f);
            }
        }
        if (this.inputRollRight != "")
        {
            if (Input.GetKeyDown(this.inputRollRight))
            {
                this.spaceship.OnTurnRoll(1.0f);
            }
        }
        if (this.inputRollLeft != "")
        {
            if (Input.GetKeyDown(this.inputRollLeft))
            {
                this.spaceship.OnTurnRoll(-1.0f);
            }
        }
    }
    #endif
}
