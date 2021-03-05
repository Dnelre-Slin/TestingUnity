using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Controllable))]
[RequireComponent(typeof(CharacterController))]
public class PlayerGravity : MonoBehaviour
{
    private Controllable controllable;

    [SerializeField]
    private bool useGravity = false;
    [SerializeField]
    private Vector3 gravityVector = Vector3.zero;
    [SerializeField]
    // private float moveCompensator = 0.01f;
    private CharacterController characterController;
    private Vector3 currentVelocity = Vector3.zero;
    private bool jumped = false;
    private Vector3 jumpVector;
    [SerializeField]
    private float jumpHeight = 1;
    // Start is called before the first frame update
    void Start()
    {
        this.controllable = GetComponent<Controllable>();
        this.characterController = GetComponent<CharacterController>();

        this.controllable.AddActionMap("Player");
        this.controllable.AddAction("Player", "Jump", ActionTypeHandler.ActionType.Performed, OnJump);

        this.jumpVector = -Mathf.Sqrt(this.jumpHeight * 2f * this.gravityVector.magnitude) * this.gravityVector.normalized;
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (this.characterController.isGrounded)
        {
            Debug.Log("JOMPalompa");
            this.jumped = true;
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (this.useGravity)
    //     {
    //         // Vector3 vel = this.characterController.velocity;
    //         Vector3 vel = Vector3.zero;
    //         vel.y = this.characterController.velocity.y;
    //         // vel += this.gravityVector * this.moveCompensator;
    //         // float y = this.gravityVector.y * Time.deltaTime;
    //         // Debug.Log("Y : " + y);
    //         // vel.y += y;
    //         vel.y += this.gravityVector.y * Time.deltaTime;
    //         // Debug.Log("GravitySystem: " + vel * Time.deltaTime);
    //         // Debug.Log("GravitySystem: " + this.characterController.velocity);
    //         // Debug.Log("GravitySystem pos: " + this.transform.position);
    //         Debug.Log("GravitySystem before: " + this.transform.position);
    //         this.characterController.Move(vel * Time.deltaTime);
    //         Debug.Log("GravitySystem after: " + this.transform.position);
    //     }
    // }
    void FixedUpdate()
    {
        if (this.useGravity)
        {
            if (this.jumped)
            {
                Debug.Log("JOMP");
                Debug.Log(this.jumpVector);
                this.currentVelocity = this.jumpVector;
                this.jumped = false;
            }
            else if (this.characterController.isGrounded)
            {
                this.currentVelocity = Vector3.zero;
            }
            this.currentVelocity += this.gravityVector * Time.fixedDeltaTime;
            this.characterController.Move(this.currentVelocity * Time.fixedDeltaTime);
        }
    }
}
