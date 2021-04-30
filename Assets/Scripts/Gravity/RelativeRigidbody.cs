using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RelativeRigidbody : MonoBehaviour
{
    protected Rigidbody rgbd = null;

    protected Vector3 relativeVelocity = Vector3.zero;
    protected Vector3 lastRgbdVelocity = Vector3.zero;

    public Vector3 velocity
    {
        get { return this.relativeVelocity; }
        set
        {
            this.relativeVelocity = value;
            this.rgbd.velocity = (this.transform.parent != null) ? this.transform.parent.rotation * this.relativeVelocity : this.relativeVelocity;
            this.lastRgbdVelocity = this.rgbd.velocity;
        }
    }

    public Vector3 realVelocity
    {
        get { return this.rgbd.velocity; }
        set
        {
            this.rgbd.velocity = value;
            this.lastRgbdVelocity = this.rgbd.velocity;
            this.relativeVelocity = (this.transform.parent != null) ? Quaternion.Inverse(this.transform.parent.rotation) * this.rgbd.velocity : this.rgbd.velocity;
        }
    }

    public Rigidbody attachedRigidbody
    {
        get { return this.rgbd; }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.rgbd = GetComponent<Rigidbody>();
        this.lastRgbdVelocity = this.rgbd.velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.lastRgbdVelocity != this.rgbd.velocity)
        {
            Debug.Log("External change!");
            this.relativeVelocity = (this.transform.parent != null) ? Quaternion.Inverse(this.transform.parent.rotation) * this.rgbd.velocity : this.rgbd.velocity;
            this.lastRgbdVelocity = this.rgbd.velocity;
        }
        else
        {
            this.rgbd.velocity = (this.transform.parent != null) ? this.transform.parent.rotation * this.relativeVelocity : this.relativeVelocity;
        }
    }
}
