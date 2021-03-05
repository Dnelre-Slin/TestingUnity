using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GravityConsumer))]
public class RigidbodyGravity : MonoBehaviour
{
    [SerializeField]
    private bool useGravity = true;
    private GravityConsumer gravityConsumer;
    private Rigidbody rgbd;
    // Start is called before the first frame update
    void Start()
    {
        this.rgbd = GetComponent<Rigidbody>();
        this.rgbd.useGravity = false;
        this.gravityConsumer = GetComponent<GravityConsumer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.useGravity)
        {
            this.rgbd.AddForce(this.gravityConsumer.GetGravity());
        }
    }
}
