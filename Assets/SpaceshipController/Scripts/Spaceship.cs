using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void OnThrustForward(float thrust)
    {
        this.thrustVelocity.z = thrust;
    }
    public void OnThrustRight(float thrust)
    {
        this.thrustVelocity.x = thrust;
    }
    public void OnThrustUp(float thrust)
    {
        this.thrustVelocity.y = thrust;
        // this.thrustVelocity.y = context.ReadValue<float>();
    }
    public void OnTurnPitchYaw(Vector2 pitchYaw)
    {
        Vector2 input = pitchYaw;
        // Vector2 input = context.ReadValue<Vector2>();
        this.turnVelocity = new Vector3(-input.y * this.pitchYawSensitivity, input.x * this.pitchYawSensitivity, this.turnVelocity.z);
    }
    public void OnTurnRoll(float roll)
    {
        this.turnVelocity.z = roll * this.rollSensitivity;
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
