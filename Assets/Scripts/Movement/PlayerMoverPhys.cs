using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverPhys : CharacterMover
{
    [SerializeField]
    private float mouseSensitivity = 5f;

    [SerializeField]
    private Camera playerCamera = null;

    private float pitchRotation = 0f;

    private bool isGrounded = false;

    [SerializeField]
    private float jumpStrength = 5f;

    private bool doJumpNext = false;

    // Temp. TODO: fix this
    //Vector3 forward;
    //Vector3 right;
    //float mouseX;
    //float mouseY;
    // End of temp

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //this.forward = Input.GetAxisRaw("Vertical") * this.transform.forward;
        //this.right = Input.GetAxisRaw("Horizontal") * this.transform.right;

        //this.mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        //this.mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            doJumpNext = true;
        }
    }

    private void FixedUpdate()
    {
        HandleHorizontalMove();
        HandleCameraMovement();
        HandleJump();
    }

    void HandleHorizontalMove()
    {
        Vector3 forward = Input.GetAxisRaw("Vertical") * this.transform.forward;
        Vector3 right = Input.GetAxisRaw("Horizontal") * this.transform.right;

        Vector3 movement = forward + right;

        //float angle = Mathf.Atan2(z, x);

        Vector2 dir = new Vector2(movement.x, movement.z).normalized;
        this.MoveHorizontal(dir);
    }

    void HandleCameraMovement()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

        this.RotateCharacterAroundUp(mouseX);

        pitchRotation -= mouseY;
        pitchRotation = Mathf.Clamp(pitchRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(pitchRotation, 0, 0);

    }

    void HandleJump()
    {
        if (doJumpNext)
        {
            doJumpNext = false;
            this.Jump(this.jumpStrength);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Grounded");
        isGrounded = true;
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Staying grounded...");
        isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Not grounded");
        isGrounded = false;
    }
}
