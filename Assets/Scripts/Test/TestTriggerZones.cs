using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTriggerZones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Entering the zone");
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("Exiting the zone");
    }
}
