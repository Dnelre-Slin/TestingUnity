﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Triggering: " + collider);
    }
}
