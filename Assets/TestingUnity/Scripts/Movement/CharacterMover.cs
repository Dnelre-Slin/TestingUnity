using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMover : MonoBehaviour
{
    private Rigidbody rgdbody;

    [SerializeField]
    private float movementSpeed = 5f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.rgdbody = GetComponent<Rigidbody>();
        Debug.Log(this.rgdbody);
    }

    public void MoveHorizontal(Vector2 dir)
    {
        dir = dir * this.movementSpeed * Time.fixedDeltaTime;
        this.rgdbody.velocity = new Vector3(dir.x, this.rgdbody.velocity.y, dir.y);
    }

    public void StopMovement()
    {
        this.rgdbody.velocity = Vector3.zero;
    }

    public void Jump(float jumpVelocity)
    {
        this.rgdbody.velocity = new Vector3(this.rgdbody.velocity.x, jumpVelocity, this.rgdbody.velocity.z);
    }

    public void RotateCharacterAroundUp(float amount)
    {
        this.transform.Rotate(Vector3.up * amount);
    }
}
