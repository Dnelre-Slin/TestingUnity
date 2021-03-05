using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionGravityProducer : BaseGravityProducer
{
    [SerializeField]
    Vector3 gravityVector = new Vector3(0f, -9.81f, 0f);
    public override Vector3 GetGravtiy(Transform consumerTransform)
    {
        return this.gravityVector;
    }
}
