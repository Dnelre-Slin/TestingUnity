using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q = Quaternion.AngleAxis(2f, new Vector3(0, 1, 0));
        this.transform.rotation = q * this.transform.rotation;
        // this.transform.position += new Vector3(0.1f, 0, 0);
    }

}
