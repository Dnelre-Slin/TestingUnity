using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGlobalPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Global pos : " + this.transform.position);
    }
}
