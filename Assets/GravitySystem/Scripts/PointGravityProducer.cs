using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGravityProducer : BaseGravityProducer
{
    [SerializeField]
    Vector3 centerOfGravity = Vector3.zero;
    [SerializeField]
    float gravitationalStrenght = 9.81f;

    public override Vector3 GetGravtiy(Transform consumerTransform)
    {
        return (this.centerOfGravity - consumerTransform.position).normalized * this.gravitationalStrenght;
    }
}
