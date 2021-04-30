using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDSExternal : MonoBehaviour
{
    [SerializeField]
    private Transform projectionZoneReal = null;
    private IDSInternal idsInternalReal = null;

    [SerializeField]
    private float _order = 1f;

    public float order
    {
        get { return this._order; }
    }

    public Transform projectionZone
    {
        get { return this.projectionZoneReal; }
    }

    public IDSInternal idsInternal
    {
        get { return this.idsInternalReal; }
        set { this.idsInternalReal = value; }
    }

    public void InitSetup(Transform projectionZone, IDSInternal idsInternal, float order)
    {
        this.projectionZoneReal = projectionZone;
        this.idsInternalReal = idsInternal;
        this._order = order;
    }

    // public void SetInternal(IDSInternal idsInternal)
    // {
    //     this.idsInternal = idsInternal;
    // }

    public void SetupTriggerArea(BoxCollider trigger)
    {
        BoxCollider triggerClone = this.gameObject.AddComponent<BoxCollider>();
        triggerClone.isTrigger = trigger.isTrigger;
        triggerClone.center = trigger.center;
        triggerClone.size = trigger.size;
    }

    void OnTriggerEnter(Collider collider)
    {
        // Debug.Log("Entering (" + this + ") : " + collider.gameObject.name);
        Rigidbody rgbd = collider.gameObject.GetComponent<Rigidbody>();

        if (rgbd != null) // Only gameobject with a rigidbody will be handled
        {
            Debug.Log("Entering (" + this + ") : " + collider.gameObject.name);
            InertialDampeningSystem idsSourceObject = collider.gameObject.GetComponent<InertialDampeningSystem>();
            if (idsSourceObject == null)
            {
                this.idsInternal.SourceObjectEnter(collider.gameObject, rgbd);
            }
            else if (idsSourceObject.idsExternal.order < this.order)
            {
                this.idsInternal.SourceObjectEnter(collider.gameObject, rgbd, idsSourceObject.idsExternal);
            }
        }
    }
}
