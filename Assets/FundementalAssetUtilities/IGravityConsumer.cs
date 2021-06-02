using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGravityConsumer
{
    Vector3 GetGravity();
    void SetDefaultGravity(Vector3 newDefaultGravity);
}
