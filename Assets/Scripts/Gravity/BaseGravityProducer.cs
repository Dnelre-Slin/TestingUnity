using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseGravityProducer : MonoBehaviour
{
    abstract public Vector3 GetGravtiy(Transform consumerTransform);
}
