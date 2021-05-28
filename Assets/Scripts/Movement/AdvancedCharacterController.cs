﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
// [RequireComponent(typeof(RelativeRigidbody))]
[RequireComponent(typeof(BaseGravityConsumer))]
public class AdvancedCharacterController : MonoBehaviour
{
    private Rigidbody rgbd;
    // private RelativeRigidbody rgbd;
    private BaseGravityConsumer gravityConsumer;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float slopeAngle = 65f;
    [SerializeField]
    private float jumpStrength = 5f;
    [SerializeField]
    private float jumpMove = 0.03f;

    private Vector3 groundNormal = Vector3.zero;
    private Vector3 gravityVelocity = Vector3.zero;
    [SerializeField]
    private bool isGrounded = false;
    private bool jumped = false;
    private Vector2 moveVector = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        this.rgbd = GetComponent<Rigidbody>();
        // this.rgbd = GetComponent<RelativeRigidbody>();
        this.gravityConsumer = GetComponent<BaseGravityConsumer>();

        this.rgbd.useGravity = false;
        // this.rgbd.attachedRigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Jump()
    {
        if (this.isGrounded)
        {
            this.jumped = true;
            Debug.Log("Jumping");
        }
    }

    public void Move(Vector2 movement)
    {
        this.moveVector = movement;
    }

    public void RotateGravityVelocity(Quaternion rotation)
    {
        this.gravityVelocity = rotation * this.gravityVelocity;
    }

    void FixedUpdate()
    {
        Vector3 gravity = this.gravityConsumer.GetGravity();
        Quaternion q = Quaternion.FromToRotation(this.transform.up, -gravity.normalized);
        this.transform.rotation = q * this.transform.rotation;
        // gravity = new Vector3(0, -9.81f, 0);

        if (this.isGrounded)
        {
            if (this.jumped)
            {
                Debug.Log("Hello???");
                this.gravityVelocity = this.jumpStrength * -gravity.normalized;
                Debug.Log(gravity.ToString("F9"));
                this.transform.position += -gravity.normalized * this.jumpMove; // Used to move character model out of ground collision, so jump can occur. Happens on jump
                this.jumped = false;
            }
            else
            {
                this.gravityVelocity = Vector3.zero;
            }
            // float gravityNormalStrength = Vector3.Dot(gravity, this.groundNormal);
            // this.gravityVelocity += gravityNormalStrength * this.groundNormal * Time.fixedDeltaTime;
        }
        else
        {
            this.gravityVelocity += gravity * Time.fixedDeltaTime;
        }

        Quaternion qn = Quaternion.FromToRotation(this.transform.up, this.groundNormal);
        Vector3 projectedForward = qn * this.transform.forward;
        Vector3 projectedRight = qn * this.transform.right;
        Debug.DrawRay(this.transform.position, projectedForward * 5f, Color.blue);
        Debug.DrawRay(this.transform.position, projectedRight * 5f, Color.red);
        Vector3 moveDirection = projectedForward * moveVector.y + projectedRight * moveVector.x;

        Vector3 moveVel = moveDirection * Time.fixedDeltaTime * this.moveSpeed;
        moveVel += this.gravityVelocity;
        this.rgbd.velocity = moveVel;
        // this.rgbd.velocity = this.transform.up * -0.5f;
        Debug.DrawRay(this.transform.position, this.gravityVelocity * 30f, Color.yellow);
        Debug.DrawRay(this.transform.position, -gravity * 20f, Color.magenta);
        Debug.DrawRay(this.transform.position, this.transform.up * 5f, Color.green);
        Debug.DrawRay(this.transform.position, this.rgbd.velocity * 5f, Color.blue);
        // // this.transform.position = this.transform.position + this.transform.up * 0.01f;
        // Debug.Log("Velocity : " + this.rgbd.velocity);
    }

    void OnCollisionStay(Collision col)
    {
        Vector3 gravity = -this.gravityConsumer.GetGravity();
        ContactPoint[] contactPoints = new ContactPoint[10]; // Hoping 10 is enough.
        int size = col.GetContacts(contactPoints);
        for (int i = 0; i < size; i++)
        {
            if (Vector3.Angle(gravity, contactPoints[i].normal) < this.slopeAngle)
            {
                this.isGrounded = true;
                this.groundNormal = contactPoints[i].normal;
                break; // One ground is good enough
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        this.isGrounded = false;
        this.groundNormal = Vector3.zero;
        // ContactPoint[] contactPoints = new ContactPoint[10]; // Hoping 10 is enough.
        // int size = col.GetContacts(contactPoints);
        // Debug.Log("Exit : " + size);
        // for (int i = 0; i < size; i++)
        // {
        //     Debug.Log("Leaving : " + Vector3.Angle(-this.gravityConsumer.GetGravity(), contactPoints[i].normal));
        //     if (Vector3.Angle(-this.gravityConsumer.GetGravity(), contactPoints[i].normal) < this.slopeAngle)
        //     {
        //         // this.isGrounded = false;
        //         // this.groundNormal = Vector3.zero;
        //         // break; // May have to fix logic, so it only sets isGrounded and groundNormal if all ground is exited.
        //     }
        // }
    }
}