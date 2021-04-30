using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestRigidbodyClone : MonoBehaviour
{
    [SerializeField]
    private Vector3 positionOffset = Vector3.zero;
    [SerializeField]
    private GameObject ignoreObject = null;
    [SerializeField]
    private Rigidbody originalRgbd = null;
    private Rigidbody rgbd = null;
    // Start is called before the first frame update
    void Start()
    {
        this.rgbd = GetComponent<Rigidbody>();
        if (this.originalRgbd != null)
        {
            this.rgbd.mass = this.originalRgbd.mass;
            // this.rgbd.useGravity = this.originalRgbd.useGravity;
            this.rgbd.useGravity = false;
            this.rgbd.constraints = this.originalRgbd.constraints;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.originalRgbd != null)
        {
            this.rgbd.position = this.originalRgbd.position + this.positionOffset;
            this.rgbd.rotation = this.originalRgbd.rotation;
            this.rgbd.transform.localScale = this.originalRgbd.transform.localScale;
            this.rgbd.velocity = this.originalRgbd.velocity;
        }
        // Debug.Log("Vel : " + this.rgbd.velocity.magnitude);
    }

    void OnCollisionStay(Collision collision)
    {

        if (this.originalRgbd != null && this.ignoreObject != collision.gameObject)
        {
        Debug.Log("Collision with : " + collision.gameObject);
        Debug.Log("Vel : " + this.rgbd.velocity.magnitude);
            this.originalRgbd.transform.position = this.rgbd.position - this.positionOffset;
            // this.originalRgbd.velocity = this.rgbd.velocity;
        //     this.originalRgbd.velocity += this.rgbd.velocity;
        }
    }
}
