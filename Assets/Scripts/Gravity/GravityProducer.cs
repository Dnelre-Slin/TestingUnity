using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GravityProducer : MonoBehaviour
{
    abstract public Vector3 GetGravtiy(Transform producerTransform);
}
