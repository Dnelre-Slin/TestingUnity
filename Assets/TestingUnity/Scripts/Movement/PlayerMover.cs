using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    private CharacterController controller;

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


    private GroundCollider groundCollider;
    private CapsuleCollider playerCollider;

    //private Vector3 movement = Vector3.zero;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        this.controller = GetComponent<CharacterController>();
        this.groundCollider = GetComponentInChildren<GroundCollider>();
        //this.playerCollider = controller.GetComponent<CapsuleCollider>();

        //CapsuleCollider gc = groundCollider.GetCapsuleCollider();
        CapsuleCollider gc = groundCollider.GetComponent<CapsuleCollider>();
        Debug.Log(gc);
        Physics.IgnoreCollision(gc, controller);

        playerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraMovement();
        HandleMovePlayer();
        HandleJump();
        HandleGravity();
        DoMove();
    }

    void HandleCameraMovement()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

        this.transform.Rotate(Vector3.up * mouseX);

        pitchRotation -= mouseY;
        pitchRotation = Mathf.Clamp(pitchRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(pitchRotation, 0, 0);
    }

    void HandleMovePlayer()
    {
        //if (this.groundCollider.grounded)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            Vector3 move = (this.transform.forward * z + this.transform.right * x).normalized * this.speed;
            this.velocity = new Vector3(move.x, this.velocity.y, move.z);
            //this.velocity = move * speed;
            //controller.Move(move * speed * Time.deltaTime);
        }
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && this.groundCollider.grounded)
        {
            Debug.Log("Jumpgin!");
            this.velocity.y = Mathf.Sqrt(this.jumpHeight * -2f * this.gravity);
            this.groundCollider.grounded = false;
        }

    }

    void HandleGravity()
    {
        if (this.groundCollider.grounded)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            //controller.Move(velocity * Time.deltaTime);
        }
    }

    void DoMove()
    {
        controller.Move(velocity * Time.deltaTime);
    }

}
