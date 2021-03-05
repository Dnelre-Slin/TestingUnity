using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseGravityConsumer : MonoBehaviour
{
    protected BaseGravityProducer currentGravityProducer;
    // Start is called before the first frame update
    virtual public Vector3 GetGravity()
    {
        // Quaternion.Slerp
        if (currentGravityProducer != null)
        {
            return currentGravityProducer.GetGravtiy(this.transform);
        }
        return Vector3.zero; // Zero-G
    }
}
