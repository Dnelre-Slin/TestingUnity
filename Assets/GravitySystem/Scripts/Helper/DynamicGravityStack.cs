using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGravityStack
{
    private LinkedList<BaseGravityProducerSizeStruct> gravityProducers = null;

    public DynamicGravityStack()
    {
        this.gravityProducers = new LinkedList<BaseGravityProducerSizeStruct>();
    }

    public BaseGravityProducer Push(BaseGravityProducer gravityProducerToAdd, float size)
    {
        BaseGravityProducerSizeStruct baseGravityProducerSizeStruct = new BaseGravityProducerSizeStruct(gravityProducerToAdd, size);
        LinkedListNode<BaseGravityProducerSizeStruct> currentNode = this.gravityProducers.Last;
        while (currentNode != null && currentNode.Value.size < size)
        {
            currentNode = currentNode.Previous;
        }
        if (currentNode == null)
        {
            this.gravityProducers.AddFirst(baseGravityProducerSizeStruct);
        }
        else
        {
            this.gravityProducers.AddAfter(currentNode, baseGravityProducerSizeStruct);
        }
        return this.gravityProducers.Last.Value.baseGravityProducer;
    }

    public BaseGravityProducer Pop(BaseGravityProducer gravityProducerToRemove)
    {
        LinkedListNode<BaseGravityProducerSizeStruct> currentNode = this.gravityProducers.Last;
        while (currentNode != null && currentNode.Value.baseGravityProducer != gravityProducerToRemove)
        {
            currentNode = currentNode.Previous;
        }
        if (currentNode != null)
        {
            if (currentNode == this.gravityProducers.Last)
            {
                this.gravityProducers.RemoveLast();
            }
            else
            {
                this.gravityProducers.Remove(currentNode);
            }
            if (this.gravityProducers.Count > 0)
            {
                return this.gravityProducers.Last.Value.baseGravityProducer;
            }
        }
        return null;
    }
}
