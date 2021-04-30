using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BaseGravityConsumer))]
public class RigidbodyGravity : MonoBehaviour
{
    [SerializeField]
    private bool useGravity = true;
    private BaseGravityConsumer gravityConsumer;
    private Rigidbody rgbd;
    // Start is called before the first frame update
    void Start()
    {
        this.rgbd = GetComponent<Rigidbody>();
        this.rgbd.useGravity = false;
        this.gravityConsumer = GetComponent<BaseGravityConsumer>();
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
