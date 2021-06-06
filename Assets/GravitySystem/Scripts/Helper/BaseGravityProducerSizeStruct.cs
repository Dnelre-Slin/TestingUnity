using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BaseGravityProducerSizeStruct
{
    public BaseGravityProducer baseGravityProducer;
    public float size;

    public BaseGravityProducerSizeStruct(BaseGravityProducer baseGravityProducer, float size)
    {
        this.baseGravityProducer = baseGravityProducer;
        this.size = size;
    }
}
