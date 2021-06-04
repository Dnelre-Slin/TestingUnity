using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Controllable))]
[RequireComponent(typeof(BaseGravityConsumer))]
public class TestColliderScript : MonoBehaviour
{
    // private CapsuleCollider capsuleCollider;
    private Rigidbody rgbd;
    private Controllable controllable;
    // private Vector3 moveDir = Vector3.zero;
    public float moveSpeed = 5f;
    public float slopeAngle = 65f;
    private Vector3 groundNormal = Vector3.zero;
    private Vector2 inputVector;
    private Vector2 inputLook;
    // [SerializeField]
    // private Vector3 gravity = new Vector3(0f, -9.81f, 0f);
    private Vector3 gravityVelocity = Vector3.zero;
    private BaseGravityConsumer gravityConsumer;
    private float mouseSensitivity = 5f;

    private float pitchRotation = 0f;

    private Camera playerCamera;

    public bool isGrounded = false;

    private bool jumped = false;
    // public bool jumpGroundEscape = false;
    public bool testMove = true;
    [SerializeField]
    private float jumpMove = 0.03f;
    // Start is called before the first frame update
    void Start()
    {
        // this.capsuleCollider = GetComponent<CapsuleCollider>();
        this.rgbd = GetComponent<Rigidbody>();
        this.gravityConsumer = GetComponent<BaseGravityConsumer>();
        this.playerCamera = GetComponentInChildren<Camera>();

        this.controllable = GetComponent<Controllable>();
        this.controllable.AddActionMap("Player");
        this.controllable.AddAction("Player", "Move", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnMove);
        this.controllable.AddAction("Player", "Look", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, OnLook);
        this.controllable.AddAction("Player", "Jump", ActionTypeHandler.ActionType.Performed, OnJump);

    }

    // Update is called once per frame
    void Update()
    {
        // HandleCameraMovement();
    }

    void OnMove(InputAction.CallbackContext context)
    {
        // Vector2 input = context.ReadValue<Vector2>();
        // moveDir = new Vector3(input.x, rgbd.velocity.y, input.y);
        this.inputVector = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        this.inputLook = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (this.isGrounded)
        {
            Debug.Log("Jumpinmg!");
            this.jumped = true;
        }
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

    void OnDrawGizmos()
    {
        // Debug.Log("Drawing gizmo at " + transform.position);
        // Gizmos.DrawWireSphere(transform.position, 5);
    }

    void FixedUpdate()
    {
        HandleCameraMovement();
        // Collider[] cols = Physics.OverlapCapsule(transform.position + transform.up, transform.position - transform.up, 0.5f);
        // Collider[] cols = Physics.OverlapSphere(transform.position, 10);
        // Debug.Log(cols.Length);
        // Debug.Log(capsuleCollider.is);
        Vector3 gravity = this.gravityConsumer.GetGravity();
        // this.transform.up = -gravity.normalized;

        // this.transform.rotation.SetLookRotation(-gravity.normalized, Vector3.up);
        // this.transform.rotation = Quaternion.FromToRotation(transform.rotation.eulerAngles, -gravity.normalized);

        // Vector3 projectedGravForward = Vector3.ProjectOnPlane(this.transform.forward, -gravity.normalized).normalized;
        // // Debug.Log("Normal ? " + projectedGravForward.magnitude);
        // Quaternion q = Quaternion.LookRotation(projectedGravForward, -gravity.normalized);
        // // Debug.Log("Q " + q);
        // this.transform.rotation = q;

        // Vector3 projectedGravForward = Vector3.ProjectOnPlane(this.transform.forward, -gravity.normalized).normalized;
        // // Debug.Log("Normal ? " + projectedGravForward.magnitude);
        Quaternion q = Quaternion.FromToRotation(this.transform.up, -gravity.normalized);
        // // if (q.w < 0.999f)
        // // {
        // //     Debug.Log("gravity : " + -gravity.normalized);
        // //     Debug.Log("up : " + this.transform.up);
        // //     Debug.Log("Quat : " + q);
        // // }
        // // Debug.DrawRay(new Vector3(-5.5f, 3.24f, 0f), q * Vector3.up * 50, Color.red);
        // Quaternion q2 = Quaternion.FromToRotation(q * this.transform.forward, projectedGravForward);
        // // q = q*q2;
        // // Debug.Log("Q " + q);
        // // if (Vector3.Angle(this.transform.up, -gravity.normalized) < 5f)
        // {
        //     Debug.Log("angle : " + Vector3.Angle(this.transform.up, -gravity.normalized));
        //     Debug.Log("gravity : " + (-gravity.normalized).ToString("F8"));
        //     Debug.Log("up : " + this.transform.up.ToString("F8"));
        //     Debug.Log("Quat : " + q.ToString("F8"));
        // }
        this.transform.rotation = q * this.transform.rotation;
        // Debug.DrawRay(this.transform.position, q * this.transform.rotation * Vector3.up * 50, Color.green);
        // Debug.DrawRay(this.transform.position, q * this.transform.rotation * Vector3.forward * 50, Color.blue);
        // Debug.DrawRay(this.transform.position, q * this.transform.rotation * Vector3.right * 50, Color.red);

        // Vector3 cross = Vector3.Cross(this.transform.up, -gravity.normalized);
        // float angle = Vector3.SignedAngle(this.transform.up, -gravity.normalized, cross);
        // Quaternion q = Quaternion.AngleAxis(-angle, cross);
        // this.transform.rotation *= q;

        // Quaternion q = Quaternion.FromToRotation(this.transform.up, -gravity.normalized);
        // this.transform.rotation *= q;

        // this.transform.LookAt(this.transform.position - gravity * 3);
        // this.transform.RotateAround(transform.position, Vector3.right, 90);
        // this.transform.rotation = ne
        // this.transform.Rotate(this.transform.up + gravity.normalized);
        // Debug.DrawRay(this.transform.position, this.transform.up * 5, Color.red);
        // Debug.DrawRay(this.transform.position, gravity.normalized * 5, Color.red);
        // Debug.DrawRay(this.transform.position, this.transform.up * 5, Color.red);
        // Vector3 gravNorm = Vector3.Cross(this.transform.up, -gravity.normalized);
        // float gravAngle = Vector3.SignedAngle(this.transform.up, -gravity.normalized, gravNorm);
        // this.transform.Rotate(gravNorm, gravAngle);

        if (isGrounded)
        {
            if (this.jumped)
            {
                // this.gravityVelocity = -Mathf.Sqrt(this.jumpHeight * 2f * this.gravityVector.magnitude) * this.gravityVector.normalized;
                float jumpStrength = 5f;
                this.gravityVelocity = jumpStrength * -gravity.normalized;
                transform.position += -gravity.normalized * jumpMove; // Used to move character model out of ground collision, so jump can occur. Happens on jump
                this.jumped = false;
                // this.jumpGroundEscape = true;
            }
            else
            // else if (!this.jumpGroundEscape)
            {
                this.gravityVelocity = Vector3.zero;
            }
            float gravityNormalStrength = Vector3.Dot(gravity, this.groundNormal);
            this.gravityVelocity += gravityNormalStrength * this.groundNormal * Time.fixedDeltaTime;
        }
        else
        {
            this.gravityVelocity += gravity * Time.fixedDeltaTime;

            // this.gravityVelocity += -this.groundNormal * Time.fixedDeltaTime;

            // Vector3 gravityTang = Vector3.ProjectOnPlane(gravity, this.groundNormal);
            // Debug.Log("Grav normal : " + (gravityNormalStrength * this.groundNormal * Time.fixedDeltaTime).ToString("F20"));
            // Debug.Log("Ground normal : " + this.groundNormal.ToString("F20"));
            // Debug.Log("Grav vel : " + this.gravityVelocity.ToString("F10"));
            // Debug.Log("Grav tang : " + gravityTang.normalized.ToString("F20"));
            // Debug.DrawRay(this.transform.position - this.transform.up, gravityTang, Color.green);
            // Vector3 gravityNormal = gravity - gravityTang;
            // this.gravityVelocity += gravityNormal * Time.fixedDeltaTime;
            // Debug.DrawRay(this.transform.position, -gravityNormal, Color.yellow);
        }

        // Vector3 moveVel = this.moveDir * Time.fixedDeltaTime * this.moveSpeed;
        Quaternion qn = Quaternion.FromToRotation(this.transform.up, this.groundNormal);
        Vector3 projectedForward = qn * this.transform.forward;
        Vector3 projectedRight = qn * this.transform.right;
        // Vector3 projectedForward = Vector3.ProjectOnPlane(this.transform.forward, this.groundNormal).normalized;
        // Vector3 projectedRight = Vector3.ProjectOnPlane(this.transform.right, this.groundNormal).normalized;
        Vector3 moveDirection = projectedForward * inputVector.y + projectedRight * inputVector.x;
        // Debug.Log("MoveDirectino : " + moveDirection.ToString("F20"));

        // Debug.DrawRay(this.transform.position, projectedForward * 3, Color.blue);
        // Debug.DrawRay(this.transform.position, projectedRight * 3, Color.red);

        Vector3 moveVel = moveDirection * Time.fixedDeltaTime * this.moveSpeed;
        if (this.testMove)
        {
            moveVel += this.gravityVelocity;
        }

        // Debug.Log("Move vel magn : " + moveVel.magnitude.ToString("F20"));
        // Debug.Log("Move vel : " + moveVel.ToString("F20"));
        // Debug.Log("Move angle : " + Vector3.Angle(moveVel, -this.groundNormal).ToString("F20"));
        Debug.DrawRay(this.transform.position, moveVel * 5, Color.magenta);
        // Debug.Log("Move vel : " + moveVel);
        // moveVel.y = rgbd.velocity.y;
        rgbd.velocity = moveVel;
        // rgbd.velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision col)
    {
        // Debug.Log("Enter");
        // // Debug.Log(col);
        // ContactPoint[] contactPoints = new ContactPoint[10];
        // // Debug.Log(col.GetContacts(contactPoints));
        // col.GetContacts(contactPoints);
        // Debug.Log(contactPoints[0].normal);
        // Debug.Log(contactPoints[0].point);
        // Debug.Log(Vector3.Angle(Vector3.up, contactPoints[0].normal));
        // Debug.DrawRay(contactPoints[0].point, 5 * contactPoints[0].normal, Color.red, 5);
        // Debug.Log(contactPoints[0].otherCollider);
        // if (Vector3.Angle(Vector3.up, contactPoints[0].normal) < this.slopeAngle)
        // {
        //     this.isGrounded = true;
        // }
        // // Debug.Log(contactPoints[0].point);
        // // Debug.Log(this.transform.InverseTransformPoint(contactPoints[0].point));
    }

    void OnCollisionStay(Collision col)
    {
        // Debug.Log("Stay");
        // Debug.Log(col);
        ContactPoint[] contactPoints = new ContactPoint[10];
        // Debug.Log(col.GetContacts(contactPoints));
        int size = col.GetContacts(contactPoints);
        // Debug.Log(contactPoints[0].normal);
        // Debug.Log(contactPoints[0].point);
        // Debug.Log(Vector3.Angle(Vector3.up, contactPoints[0].normal));
        // Debug.Log(contactPoints[0].otherCollider);
        for (int i = 0; i < size; i++)
        {
            // if (Vector3.Angle(Vector3.up, contactPoint.normal) < this.slopeAngle)
            if (Vector3.Angle(-this.gravityConsumer.GetGravity(), contactPoints[i].normal) < this.slopeAngle)
            {
                // Debug.Log("Inverse gravity : " + (-this.gravityConsumer.GetGravity()).ToString("F8"));
                // Debug.Log("contact normal : " + contactPoint.normal.ToString("F8"));
                // Debug.Log("Ground angle : " + Vector3.Angle(-this.gravityConsumer.GetGravity(), contactPoint.normal).ToString("F8"));
                // Debug.DrawRay(contactPoint.point, 5 * contactPoint.normal, Color.red);
                this.isGrounded = true;
                // this.rgbd.useGravity = false;
                this.groundNormal = contactPoints[i].normal;
                // Debug.Log("Ground normaled : " + this.groundNormal.ToString("F5"));
                break; // One ground is good enough
            }
        }
        // Debug.Log(contactPoints[0].point);
        // Debug.Log(this.transform.InverseTransformPoint(contactPoints[0].point));
    }

    void OnCollisionExit(Collision col)
    {
        Debug.Log("Exit");
        this.isGrounded = false;
        // this.jumpGroundEscape = false;
        // this.rgbd.useGravity = true;
        // this.groundNormal = this.transform.up;
        this.groundNormal = Vector3.zero;
        // // Debug.Log(col);
        // ContactPoint[] contactPoints = new ContactPoint[10];
        // // Debug.Log(col.GetContacts(contactPoints));
        // col.GetContacts(contactPoints);
        // Debug.Log(contactPoints[0].normal);
        // Debug.Log(contactPoints[0].point);
        // // Debug.Log(contactPoints[0].point);
        // // Debug.Log(this.transform.InverseTransformPoint(contactPoints[0].point));
    }
}
