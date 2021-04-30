using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Controllable))]
public class PlayerMover2 : MonoBehaviour
{
    private CharacterController controller;
    private Controllable controllable;

    // [SerializeField]
    // private float groundedDownSpeed = 2f;

    [SerializeField]
    private float speed = 12f;
    // [SerializeField]
    // private float runSpeed = 2f;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpHeight = 3f;

    [SerializeField]
    private float mouseSensitivity = 300f;
    private float pitchRotation = 0f;
    private Camera playerCamera;

    private Vector2 inputMove;
    private Vector2 inputLook;

    private Vector3 velocity = Vector3.zero;

    private float jumpSpeed = 0f;

    public SpaceshipController spaceship;

    public bool applyGravity = false;

    // Start is called before the first frame update
    void Start()
    {
        this.controller = GetComponent<CharacterController>();

        this.playerCamera = GetComponentInChildren<Camera>();

        this.controllable = GetComponent<Controllable>();

        this.controllable.AddActionMap("Player");
        this.controllable.AddAction("Player", "Move", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnMove);
        this.controllable.AddAction("Player", "Look", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnLook);
        this.controllable.AddAction("Player", "Jump", ActionTypeHandler.ActionType.Performed, OnJump);

        // this.transform.SetParent(this.spaceship.transform);
        // ParentConstraint parentConstraint = this.gameObject.AddComponent<ParentConstraint>();
        // ConstraintSource constraintSource = new ConstraintSource();
        // constraintSource.sourceTransform = spaceship.transform;
        // constraintSource.weight = 1;
        // parentConstraint.AddSource(constraintSource);
        // parentConstraint.constraintActive = true;

        // foreach (Component mono in GetComponents<Component>())
        // {
        //     if (mono is Collider)
        //     {
        //         ((Collider)mono).enabled = false;
        //     }
        //     else if (mono is MonoBehaviour)
        //     {
        //         ((MonoBehaviour)mono).enabled = false;
        //     }
        //     else if (mono is Rigidbody)
        //     {
        //         ((Rigidbody)mono).isKinematic = true;
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraMovement();
        HandleMovePlayer();
        HandleGravity();
        DoMove();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        this.inputLook = context.ReadValue<Vector2>();
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

    public void OnMove(InputAction.CallbackContext context)
    {
        this.inputMove = context.ReadValue<Vector2>();
    }

    void HandleMovePlayer()
    {
        float x = this.inputMove.x;
        float z = this.inputMove.y;

        Vector3 move = (this.transform.forward * z + this.transform.right * x).normalized * this.speed;
        this.velocity = new Vector3(move.x, this.velocity.y, move.z);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (this.controller.isGrounded)
        {
            Debug.Log("Jimp");
            this.jumpSpeed = Mathf.Sqrt(this.jumpHeight * -2f * this.gravity);
            // this.velocity.y = this.jumpSpeed;
        }
    }

    void HandleGravity()
    {
        // this.velocity.y = this.controller.velocity.y;
        this.velocity.y = 0;

        // bool skip = true;

        if (this.jumpSpeed > 0f)
        {
            this.transform.position += new Vector3(0f, 0.3f, 0f);
            this.velocity.y = this.jumpSpeed;
            this.jumpSpeed = 0f;
            // skip = false;
        }
        // if (applyGravity)
        // {
        //     if (skip && this.controller.isGrounded)
        //     {
        //         velocity.y = 0;
        //     }
        //     this.velocity.y += this.gravity * Time.deltaTime;
        // }

        else if (this.controller.isGrounded)
        {
            // velocity.y = -this.groundedDownSpeed;
            velocity.y = 0;
        }
        if (applyGravity)
        {
            // float y = gravity * Time.deltaTime;
            // Debug.Log("Y : " + y);
            // velocity.y += y;
            // this.velocity.y = this.controller.velocity.y;
            this.velocity.y += this.gravity * Time.deltaTime;
            // Debug.Log("Player2 : " + this.controller.velocity);
        }
    }

    void DoMove()
    {
        if (applyGravity)
        {
            // Debug.Log("Player2 : " + this.controller.velocity);
            // Debug.Log("Player2 pos : " + this.transform.position);
            // Debug.Log("Player2 before : " + this.transform.position);
        }
            // Debug.Log("Player2 : " + this.controller.velocity);
        controller.Move(velocity * Time.deltaTime);
            // Debug.Log("Player2 after: " + this.controller.velocity);
        if (applyGravity)
        {
            // Debug.Log("Player2 after : " + this.transform.position);
        }
    }
}
