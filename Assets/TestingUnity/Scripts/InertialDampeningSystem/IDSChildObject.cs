using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IDSChildObject
{
    public Rigidbody rigidbody;
    public List<Collider> colliders;
    public IDSChildObject(Rigidbody rigidbody)
    {
        this.rigidbody = rigidbody;
        this.colliders = new List<Collider>(rigidbody.GetComponentsInChildren<Collider>());
    }
    public IDSChildObject(GameObject gameObject, Rigidbody rigidbody)
    {
        this.rigidbody = rigidbody;
        this.colliders = new List<Collider>(gameObject.GetComponentsInChildren<Collider>());
    }
    public IDSChildObject(GameObject gameObject)
    {
        this.rigidbody = gameObject.GetComponent<Rigidbody>();
        this.colliders = new List<Collider>(gameObject.GetComponentsInChildren<Collider>());
    }
}
