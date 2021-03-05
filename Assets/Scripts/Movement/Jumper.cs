using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float runSpeed = 2f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float groundDistance = 0.5f;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity);

        Debug.Log(hit.distance - groundDistance);

        isGrounded = hit.distance < groundDistance;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float useRunSpeed = 1f;
        //if (Input.GetKey(KeyCode.LeftShift))
        if (Input.GetButton("Fire3"))
        {
            useRunSpeed = runSpeed;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * useRunSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            //velocity.y += jumpHeight;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
