using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private Vector3 maxThurstSpeeds = new Vector3(100f, 100f, 100f);
    [SerializeField]
    private Vector3 thrustAcceleration = new Vector3(10f, 10f, 10f);

    [SerializeField]
    private float pitchYawSensitivity = 10f;
    [SerializeField]
    private float rollSensitivity = 40f;
    [SerializeField]
    private float mainThrustSpeedAcceleration = 5f;
    [SerializeField]
    private float mainThrustMaxSpeed = 1000f;
    [SerializeField]
    private float mainThrustMinSpeed = -1000f;

    private Rigidbody rgbd;
    private Vector3 thrustInputVelocity = Vector3.zero;
    private Vector3 turnVelocity = Vector3.zero;
    [SerializeField]
    private bool landingModeActive = true;
    [SerializeField]
    private float desiredMainThrustSpeed = 0.0f;


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
        HandleUpdateMainThrustSpeed();
        HandleTurn();
    }

    public void OnThrustForward(float thrust)
    {
        this.thrustInputVelocity.z = thrust;
    }
    public void OnThrustRight(float thrust)
    {
        this.thrustInputVelocity.x = thrust;
    }
    public void OnThrustUp(float thrust)
    {
        this.thrustInputVelocity.y = thrust;
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

    public void ToggleLandingMode()
    {
        this.landingModeActive = !this.landingModeActive;
        this.desiredMainThrustSpeed = 0.0f; // Set this to 0, when toggleing landing mode.
    }

    public void SetDesiredMainThrustSpeed(float desiredMainThrustSpeed)
    {
        this.desiredMainThrustSpeed = desiredMainThrustSpeed;
    }

    public void ZeroMainThrustSpeed()
    {
        this.desiredMainThrustSpeed = 0.0f;
    }

    public void ClearInputs()
    {
        this.thrustInputVelocity = Vector3.zero;
        this.turnVelocity = Vector3.zero;
    }

    void HandleThrust()
    {
        Vector3 thrustVelocity = Vector3.Scale(this.thrustInputVelocity, this.maxThurstSpeeds);
        if (!this.landingModeActive)
        {
            thrustVelocity.z = this.desiredMainThrustSpeed;
        }
        Vector3 desiredVelocity = this.transform.TransformDirection(thrustVelocity);

        Vector3 currentVelocity = VectorCalculations.GradualVector3Change(this.rgbd.velocity / Time.fixedDeltaTime, desiredVelocity, this.thrustAcceleration);

        this.rgbd.velocity = currentVelocity * Time.fixedDeltaTime;
    }

    void HandleTurn()
    {
        this.transform.Rotate(this.turnVelocity * Time.deltaTime);
    }

    void HandleUpdateMainThrustSpeed()
    {
        if (!this.landingModeActive)
        {
            this.desiredMainThrustSpeed += this.mainThrustSpeedAcceleration * this.thrustInputVelocity.z;
            this.desiredMainThrustSpeed = Mathf.Clamp(this.desiredMainThrustSpeed, this.mainThrustMinSpeed, this.mainThrustMaxSpeed);
        }
    }

}
