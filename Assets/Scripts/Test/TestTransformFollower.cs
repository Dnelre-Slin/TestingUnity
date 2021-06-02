using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public struct IDSChildObject
// {
//     public Rigidbody rigidbody;
//     public List<Collider> colliders;
//     public IDSChildObject(Rigidbody rigidbody)
//     {
//         this.rigidbody = rigidbody;
//         this.colliders = new List<Collider>(rigidbody.GetComponentsInChildren<Collider>());
//     }
//     public IDSChildObject(GameObject gameObject, Rigidbody rigidbody)
//     {
//         this.rigidbody = rigidbody;
//         this.colliders = new List<Collider>(gameObject.GetComponentsInChildren<Collider>());
//     }
//     public IDSChildObject(GameObject gameObject)
//     {
//         this.rigidbody = gameObject.GetComponent<Rigidbody>();
//         this.colliders = new List<Collider>(gameObject.GetComponentsInChildren<Collider>());
//     }
// }

public class TestTransformFollower : MonoBehaviour
{
    [SerializeField]
    private Transform otherTransform = null;
    [SerializeField]
    private Rigidbody externalRigidbody = null;
    [SerializeField]
    private List<Collider> externalColliders = new List<Collider>();
    // private List<Rigidbody> childrenRigidbodies = new List<Rigidbody>();
    // private List<Collider> childrenColliders = new List<Collider>();
    private Dictionary<GameObject, IDSChildObject> childObjects = null;

    void Start()
    {
        this.childObjects = new Dictionary<GameObject, IDSChildObject>(10);
        // Ignore all collision between internal and exterior shell
        Collider[] internalColliders = this.GetComponentsInChildren<Collider>();
        this.IgnoreCollisionWithExternal(internalColliders);
    }

    void FixedUpdate()
    {
        this.transform.position = this.otherTransform.position;

        if (this.transform.rotation != this.otherTransform.rotation)
        {
            Quaternion rotation = this.otherTransform.rotation * Quaternion.Inverse(this.transform.rotation);
            this.transform.rotation = this.otherTransform.rotation;
            this.RotateChildVelocities(rotation);
        }
    }

    void RotateChildVelocities(Quaternion rotation)
    {
        foreach (var childObject in this.childObjects.Values)
        {
            // Debug.Log(childObject.rigidbody);
            childObject.rigidbody.angularVelocity = rotation * childObject.rigidbody.angularVelocity;
            childObject.rigidbody.velocity = rotation * childObject.rigidbody.velocity;
        }
    }

    void IgnoreCollisionBetweenColliders(IEnumerable<Collider> colliders1, IEnumerable<Collider> colliders2, bool ignore = true)
    {
        foreach (var col1 in colliders1)
        {
            foreach (var col2 in colliders2)
            {
                Physics.IgnoreCollision(col1, col2, ignore);
            }
        }
    }

    void IgnoreCollisionWithExternal(IEnumerable<Collider> colliders, bool ignore = true)
    {
        foreach (var colExternal in this.externalColliders)
        {
            foreach (var col in colliders)
            {
                Physics.IgnoreCollision(colExternal, col, ignore);
            }
        }
    }

    bool IsRigidbodyLinkedToIDS(Rigidbody rigidbody)
    {
        return this.childObjects.ContainsKey(rigidbody.gameObject);
    }

    void AddObjectToIDS(Rigidbody rigidbody)
    {
        GameObject go = rigidbody.gameObject;
        go.transform.parent = this.transform;

        IDSChildObject childObject = new IDSChildObject(rigidbody);
        this.childObjects[go] = childObject;
        this.IgnoreCollisionWithExternal(childObject.colliders);
    }

    void RemoveObjectFromIDS(Rigidbody rigidbody)
    {
        GameObject go = rigidbody.gameObject;
        // go.transform.parent = this.transform.parent;
        go.transform.parent = null; // Until above is fixed. Needs to be grandparent, or even better, do something smarter

        IDSChildObject childObject = this.childObjects[go];
        this.IgnoreCollisionWithExternal(childObject.colliders, false); // False to un-ignore
        this.childObjects.Remove(go);
    }

    void OnTriggerEnter(Collider collider)
    {
        Rigidbody colRgbd = collider.GetComponentInParent<Rigidbody>();
        if (colRgbd != null && colRgbd != externalRigidbody)
        {
            // Dock to IDS system
            this.AddObjectToIDS(colRgbd);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Rigidbody colRgbd = collider.GetComponentInParent<Rigidbody>();
        if (colRgbd != null && colRgbd != externalRigidbody && this.IsRigidbodyLinkedToIDS(colRgbd))
        {
            // Undock with IDS system
            this.RemoveObjectFromIDS(colRgbd);
        }
    }
}
