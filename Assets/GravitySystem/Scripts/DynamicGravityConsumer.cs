using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGravityConsumer : BaseGravityConsumer
{
    void OnTriggerEnter(Collider collider)
    {
        BaseGravityProducer gravityProducer = collider.GetComponent<BaseGravityProducer>();
        if (gravityProducer != null)
        {
        Debug.Log("OnTriggerEnter : " + collider.gameObject.name);
            currentGravityProducer = gravityProducer;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        BaseGravityProducer gravityProducer = collider.GetComponent<BaseGravityProducer>();
        // if (gravityProducer != null)
        // {
        // Debug.Log("OnTriggerExit : " + collider.gameObject.name);
        // // Stack<int> s = new Stack<int>();
        // // List<int> l = new List<int>();
        // // l.

        // }
        if (gravityProducer != null && currentGravityProducer == gravityProducer)
        {
            currentGravityProducer = null;
        }
    }
}
