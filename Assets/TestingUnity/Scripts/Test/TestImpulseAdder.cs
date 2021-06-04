using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestImpulseAdder : MonoBehaviour
{
    [SerializeField]
    Vector3 force = Vector3.zero;
    [SerializeField]
    bool impulse = true;
    [SerializeField]
    bool execute = false;

    Rigidbody rgbd = null;
    // Start is called before the first frame update
    void Start()
    {
        rgbd = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Debug.Log("Vel: " + this.rgbd.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.execute)
        {
            this.Execute();
            this.execute = false;
        }
    }

    public void Execute()
    {
        ForceMode forceMode = ForceMode.Force;
        if (this.impulse)
        {
            forceMode = ForceMode.Impulse;
        }
        this.rgbd.AddForce(force, forceMode);
    }
}
