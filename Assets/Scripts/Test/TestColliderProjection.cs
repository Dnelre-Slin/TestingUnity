using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColliderProjection : MonoBehaviour
{
    [SerializeField]
    private Vector3 transformOffset = Vector3.zero;
    [SerializeField]
    private Rigidbody rgbd = null;

    private Transform otherTransform = null;
    // Start is called before the first frame update
    void Start()
    {
        if (this.rgbd != null)
        {
            this.otherTransform = this.rgbd.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.otherTransform != null)
        {
            this.transform.position = this.otherTransform.position + this.transformOffset;
            this.transform.rotation = this.otherTransform.rotation;
            this.transform.localScale = this.otherTransform.localScale;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision1! " + collision.gameObject);
        // Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        Rigidbody otherRigidbody = collision.rigidbody;
        if (otherRigidbody != null)
        {
            Debug.Log("Collision! " + collision.gameObject);
            List<ContactPoint> contacts = new List<ContactPoint>(collision.contactCount);
            collision.GetContacts(contacts);
            foreach (var contact in contacts)
            {
                // Debug.DrawRay(contact.point, contact.normal*10);
                Debug.DrawRay(contact.point - this.transformOffset, contact.normal*10);
                Debug.Log("Vel : " + ((otherRigidbody.velocity.magnitude / Time.deltaTime)) * otherRigidbody.mass);
                // this.rgbd.AddForceAtPosition(contact.normal * 1000, contact.point - this.transformOffset, ForceMode.Force);
                // break;
            }
        }
    }
}
