using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGravityConsumer : BaseGravityConsumer
{
    [SerializeField]
    protected BaseGravityProducer staticGravityProducer = null;
    // Start is called before the first frame update
    void Start()
    {
        this.currentGravityProducer = staticGravityProducer;
    }

    public void SetGravityProducer(BaseGravityProducer newGravityProducer)
    {
        this.currentGravityProducer = staticGravityProducer;
    }
}
