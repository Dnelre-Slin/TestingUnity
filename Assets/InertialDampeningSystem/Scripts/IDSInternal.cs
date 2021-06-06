using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDSInternal : MonoBehaviour
{
    private Transform externalTransform = null;
    private Rigidbody externalRigidbody = null;
    private Collider[] externalColliders = null;
    private Dictionary<GameObject, IDSChildObject> childObjects = null;
    private IdsDoorUpdater idsDoorUpdater = null;
    private Stack<ColliderPair> ignoredExternalCollidersStack = null;
    private bool performStackClear = false;
    private float clearStackIntervalTime = 10.0f; // Maybe serialize or set in setup

    void Start()
    {
        StartCoroutine("ExternalStackClearTimer");
    }

    public void Setup(Transform externalTransform, Rigidbody externalRigidbody, Collider[] externalColliders, IdsDoorUpdater idsDoorUpdater)
    {
        this.externalTransform = externalTransform;
        this.externalRigidbody = externalRigidbody;
        this.externalColliders = externalColliders;
        this.idsDoorUpdater = idsDoorUpdater;
        this.ignoredExternalCollidersStack = new Stack<ColliderPair>(10); // See comment just below, same here.
        this.childObjects = new Dictionary<GameObject, IDSChildObject>(10); // Trying with 10 as a start size. Maybe smarter to give user control of the start size
        // Ignore all collision between internal and exterior shell
        Collider[] internalColliders = this.GetComponentsInChildren<Collider>();
        this.IgnoreCollisionWithExternal(internalColliders);
    }

    void Update()
    {
        if (this.performStackClear)
        {
            this.ClearExternalColliderStack();
            this.performStackClear = false;
        }

        // Move internal transform to external transforms posistion:

        this.transform.position = this.externalTransform.position;

        if (this.transform.rotation != this.externalTransform.rotation)
        {
            Quaternion rotation = this.externalTransform.rotation * Quaternion.Inverse(this.transform.rotation);
            this.transform.rotation = this.externalTransform.rotation;
            this.RotateChildVelocities(rotation);
        }

        idsDoorUpdater.UpdateDoors();
    }

    void RotateChildVelocities(Quaternion rotation)
    {
        foreach (var childObject in this.childObjects.Values)
        {
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

    IEnumerator ExternalStackClearTimer()
    {
        while (true)
        {
            this.performStackClear = true;
            yield return new WaitForSeconds(this.clearStackIntervalTime);
        }
    }

    void ClearExternalColliderStack()
    {
        while (this.ignoredExternalCollidersStack.Count > 0)
        {
            ColliderPair colliderPair = this.ignoredExternalCollidersStack.Pop();
            Physics.IgnoreCollision(colliderPair.collider1, colliderPair.collider2, false);
        }
    }

    void IgnoreExternalObjectCollisions(Collision externalObjectCollision)
    {
        ContactPoint[] contactPoints = new ContactPoint[externalObjectCollision.contactCount];
        int size = externalObjectCollision.GetContacts(contactPoints);
        for (int i = 0; i < size; i++)
        {
            ColliderPair colliderPair = new ColliderPair(contactPoints[i].thisCollider, contactPoints[i].otherCollider);
            if (!Physics.GetIgnoreCollision(colliderPair.collider1, colliderPair.collider2))
            {
                Physics.IgnoreCollision(colliderPair.collider1, colliderPair.collider2);
                this.ignoredExternalCollidersStack.Push(colliderPair);
            }
        }
        externalObjectCollision.rigidbody.AddForce(-externalObjectCollision.impulse, ForceMode.Impulse);
    }

    void AddObjectToIDS(Rigidbody rigidbody)
    {
        GameObject go = rigidbody.gameObject;
        go.transform.parent = this.transform;

        rigidbody.velocity -= this.externalRigidbody.velocity;
        go.transform.position -= this.externalRigidbody.transform.position - this.transform.position;

        IDSChildObject childObject = new IDSChildObject(rigidbody);
        this.childObjects[go] = childObject;
        this.IgnoreCollisionWithExternal(childObject.colliders);
        this.performStackClear = true;
    }

    void RemoveObjectFromIDS(Rigidbody rigidbody)
    {
        GameObject go = rigidbody.gameObject;
        // go.transform.parent = this.transform.parent;
        go.transform.parent = null; // Until above is fixed. Needs to be grandparent, or even better, do something smarter

        rigidbody.velocity += this.externalRigidbody.velocity;
        go.transform.position += this.externalRigidbody.transform.position - this.transform.position;

        IDSChildObject childObject = this.childObjects[go];
        this.IgnoreCollisionWithExternal(childObject.colliders, false); // False to un-ignore
        this.childObjects.Remove(go);
    }

    void OnTriggerEnter(Collider collider)
    {
        Rigidbody colRgbd = collider.GetComponentInParent<Rigidbody>();
        if (colRgbd != null && !colRgbd.isKinematic && colRgbd != externalRigidbody)
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

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody colRgbd = collision.rigidbody;
        if (!this.IsRigidbodyLinkedToIDS(colRgbd))
        {
            this.IgnoreExternalObjectCollisions(collision);
        }
    }
}
