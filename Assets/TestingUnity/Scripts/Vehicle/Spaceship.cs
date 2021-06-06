using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private Vector3 thurstSpeeds = new Vector3(100f, 100f, 100f);

    [SerializeField]
    private float pitchYawSensitivity = 10f;
    [SerializeField]
    private float rollSensitivity = 40f;

    private Rigidbody rgbd;
    private Vector3 thrustVelocity;
    private Vector3 turnVelocity;


    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleThrust();
    }
    void Update()
    {
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

    public void OnFullStop()
    {
        this.thrustVelocity = Vector3.zero;
        this.turnVelocity = Vector3.zero;
    }

    void HandleThrust()
    {
        this.rgbd.velocity = this.transform.TransformDirection(Vector3.Scale(this.thrustVelocity, this.thurstSpeeds) * Time.fixedDeltaTime);
    }

    void HandleTurn()
    {
        this.transform.Rotate(this.turnVelocity * Time.deltaTime);
    }
}
