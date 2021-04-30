using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMover : MonoBehaviour
{
    [SerializeField]
    private Vector3 initVelocity = Vector3.zero;
    [SerializeField]
    private bool useRigidbody = false;
    // Start is called before the first frame update
    void Start()
    {
        if (this.useRigidbody)
        {
            Rigidbody rgbd = GetComponent<Rigidbody>();
            if (rgbd != null)
            {
                rgbd.velocity = initVelocity;
            }
        }
    }

    void Update()
    {
        if (!this.useRigidbody)
        {
            this.transform.position += initVelocity * Time.deltaTime;
        }
    }
}
