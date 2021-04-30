using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRelativeDirectionGravityProducer : DirectionGravityProducer
{
    [SerializeField]
    private Transform transformObject = null;

    public override Vector3 GetGravtiy(Transform consumerTransform)
    {
        return this.transformObject.rotation * this.gravityVector;
    }
}
