using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityConsumer : MonoBehaviour
{
    private GravityProducer currentGravityProducer;

    virtual public Vector3 GetGravity()
    {
        // Quaternion.Slerp
        if (currentGravityProducer != null)
        {
            // return currentGravityProducer.GetGravtiy(this.transform);
        }
        return Vector3.zero; // Zero-G
    }

    void OnTriggerEnter(Collider collider)
    {
        GravityProducer gravityProducer = collider.GetComponent<GravityProducer>();
        if (gravityProducer != null)
        {
            Debug.Log("Gravity!");
            currentGravityProducer = gravityProducer;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        GravityProducer gravityProducer = collider.GetComponent<GravityProducer>();
        if (gravityProducer != null && currentGravityProducer == gravityProducer)
        {
            currentGravityProducer = null;
        }
    }
}
