using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    private CapsuleCollider groundCollider;
    public bool grounded { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        groundCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CapsuleCollider GetCapsuleCollider()
    {
        return this.groundCollider;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag != "Player")
        {
            this.grounded = true;
            Debug.Log("Grounded");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.tag != "Player")
        {
            //this.grounded = true;
            //Debug.Log("Grounded");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.grounded = false;
        Debug.Log("Airbourne");
    }
}
