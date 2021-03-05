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
            currentGravityProducer = gravityProducer;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        BaseGravityProducer gravityProducer = collider.GetComponent<BaseGravityProducer>();
        if (gravityProducer != null && currentGravityProducer == gravityProducer)
        {
            currentGravityProducer = null;
        }
    }
}
