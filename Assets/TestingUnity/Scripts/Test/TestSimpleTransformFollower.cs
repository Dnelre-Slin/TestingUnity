using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSimpleTransformFollower : MonoBehaviour
{
    [SerializeField]
    Transform transformToFollow = null;

    Collider col = null;

    void Start()
    {
        this.col = this.GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        this.transform.position = this.transformToFollow.position;
        this.transform.rotation = this.transformToFollow.rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        Physics.IgnoreCollision(col, collision.collider);
        collision.rigidbody.AddForce(-collision.impulse, ForceMode.Impulse);
    }
}
