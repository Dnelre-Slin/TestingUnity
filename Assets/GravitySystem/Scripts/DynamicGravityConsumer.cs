using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGravityConsumer : BaseGravityConsumer
{
    private DynamicGravityStack dynamicGravityStack = new DynamicGravityStack();

    void OnTriggerEnter(Collider collider)
    {
        BaseGravityProducer gravityProducer = collider.GetComponent<BaseGravityProducer>();
        if (gravityProducer != null)
        {
            this.currentGravityProducer = this.dynamicGravityStack.Push(gravityProducer, collider.bounds.size.sqrMagnitude);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        BaseGravityProducer gravityProducer = collider.GetComponent<BaseGravityProducer>();
        if (gravityProducer != null)
        {
            this.currentGravityProducer = this.dynamicGravityStack.Pop(gravityProducer);
        }
    }
}
