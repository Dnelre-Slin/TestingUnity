using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Controllable))]
public class SpaceshipController : MonoBehaviour
{
    private Controllable controllable;
    private Rigidbody rgbd;

    private Vector3 thrustVelocity;
    private Vector3 turnVelocity;

    [SerializeField]
    private Vector3 thurstSpeeds = new Vector3(100f, 100f, 100f);

    [SerializeField]
    private float pitchYawSensitivity = 10f;
    [SerializeField]
    private float rollSensitivity = 40f;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();

        this.controllable = GetComponent<Controllable>();

        this.controllable.AddActionMap("Spaceship");
        this.controllable.AddAction("Spaceship", "ThrustForward", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnThrustForward);
        this.controllable.AddAction("Spaceship", "ThrustRight", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnThrustRight);
        this.controllable.AddAction("Spaceship", "ThrustUp", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnThrustUp);
        this.controllable.AddAction("Spaceship", "TurnPitchYaw", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnTurnPitchYaw);
        this.controllable.AddAction("Spaceship", "TurnRoll", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnTurnRoll);
        this.controllable.AddAction("Spaceship", "ExitVehicle", ActionTypeHandler.ActionType.Performed, OnExitVehicle);

    }

    // Update is called once per frame
    void Update()
    {
        HandleThrust();
        HandleTurn();
    }

    public void OnThrustForward(InputAction.CallbackContext context)
    {
        this.thrustVelocity.z = context.ReadValue<float>();
    }
    public void OnThrustRight(InputAction.CallbackContext context)
    {
        this.thrustVelocity.x = context.ReadValue<float>();
    }
    public void OnThrustUp(InputAction.CallbackContext context)
    {
        this.thrustVelocity.y = context.ReadValue<float>();
    }
    public void OnTurnPitchYaw(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        this.turnVelocity = new Vector3(-input.y * this.pitchYawSensitivity, input.x * this.pitchYawSensitivity, this.turnVelocity.z);
    }
    public void OnTurnRoll(InputAction.CallbackContext context)
    {
        this.turnVelocity.z = -context.ReadValue<float>() * this.rollSensitivity;
    }

    public void OnUnpossesed()
    {
        this.thrustVelocity = Vector3.zero;
        this.turnVelocity = Vector3.zero;
    }

    void HandleThrust()
    {
        this.rgbd.velocity = this.transform.TransformDirection(Vector3.Scale(this.thrustVelocity, this.thurstSpeeds) * Time.deltaTime);
    }

    void HandleTurn()
    {
        this.transform.Rotate(this.turnVelocity * Time.fixedDeltaTime);
    }

    public void OnExitVehicle(InputAction.CallbackContext context)
    {
        this.controllable.Unpossess();
        this.OnUnpossesed(); // Consider moving this to a controllable subscription.
    }
}
