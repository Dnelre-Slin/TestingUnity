using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShowVelocity : MonoBehaviour
{
    [SerializeField]
    private Vector3 velocity;

    private Rigidbody rgbd;
    // Start is called before the first frame update
    void Start()
    {
        this.rgbd = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.velocity = this.rgbd.velocity;
    }
}
