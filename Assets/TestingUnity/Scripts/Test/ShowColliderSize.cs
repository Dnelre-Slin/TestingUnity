using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowColliderSize : MonoBehaviour
{
    [SerializeField]
    private Collider _collider = null;

    void Update()
    {
        if (this._collider != null)
        {
            Debug.Log("Collider ( " + this._collider + " ) size : " + this._collider.bounds.size.sqrMagnitude);
        }
    }
}
