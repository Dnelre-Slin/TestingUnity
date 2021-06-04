using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this + " collided with");
        Debug.Log("Collision Impulse : " + collision.impulse);
    }
}
