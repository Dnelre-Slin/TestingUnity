#if (ENABLE_INPUT_SYSTEM)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Spaceship), typeof(InputSystemControlScheme), typeof(Controllable))]
public class SpaceshipController : MonoBehaviour
{
    private Spaceship spaceship;
    private Controllable controllable;
    private InputSystemControlScheme controlScheme;

    void Start()
    {
        this.spaceship = GetComponent<Spaceship>();

        this.controllable = GetComponent<Controllable>();
        this.controlScheme = GetComponent<InputSystemControlScheme>();

        this.controlScheme.AddActionMap("Spaceship");
        this.controlScheme.AddAction("Spaceship", "ThrustForward", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, ctx => this.spaceship.OnThrustForward(ctx.ReadValue<float>()));
        this.controlScheme.AddAction("Spaceship", "ThrustRight", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, ctx => this.spaceship.OnThrustRight(ctx.ReadValue<float>()));
        this.controlScheme.AddAction("Spaceship", "ThrustUp", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, ctx => this.spaceship.OnThrustUp(ctx.ReadValue<float>()));
        this.controlScheme.AddAction("Spaceship", "TurnPitchYaw", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, ctx => this.spaceship.OnTurnPitchYaw(ctx.ReadValue<Vector2>()));
        this.controlScheme.AddAction("Spaceship", "TurnRoll", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, ctx => this.spaceship.OnTurnRoll(ctx.ReadValue<float>()));
        this.controlScheme.AddAction("Spaceship", "ToggleLandingMode", ActionTypeHandler.ActionType.Performed, ctx => this.spaceship.ToggleLandingMode());
        this.controlScheme.AddAction("Spaceship", "ExitVehicle", ActionTypeHandler.ActionType.Performed, OnExitVehicle);
    }

    public void OnExitVehicle(InputAction.CallbackContext context)
    {
        this.controllable.Unpossess();
        this.spaceship.OnFullStop(); // Consider moving this to a controllable subscription.
    }
}

#endif