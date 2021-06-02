using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseGravityConsumer : MonoBehaviour, IGravityConsumer
{
    [SerializeField]
    protected Vector3 defaultGravity = Vector3.zero;
    protected BaseGravityProducer currentGravityProducer;

    virtual public Vector3 GetGravity()
    {
        // Quaternion.Slerp
        if (currentGravityProducer != null)
        {
            return currentGravityProducer.GetGravtiy(this.transform);
        }
        return this.defaultGravity;
    }

    virtual public void SetDefaultGravity(Vector3 newDefaultGravity)
    {
        this.defaultGravity = newDefaultGravity;
    }
}
