using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ColliderPair
{
    public Collider collider1;
    public Collider collider2;
    public ColliderPair(Collider collider1, Collider collider2)
    {
        this.collider1 = collider1;
        this.collider2 = collider2;
    }
}
