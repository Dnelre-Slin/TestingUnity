using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHeirarchy : MonoBehaviour
{
    private Rigidbody rgbd;
    [SerializeField]
    private Transform parent = null;
    [SerializeField]
    private Transform child = null;
    public bool flip = false;
    // Start is called before the first frame update
    void Start()
    {
        this.rgbd = GetComponent<Rigidbody>();
        // this.rgbd.velocity = new Vector3(0,0,1f);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Grandparent rotation : " + this.transform.rotation.ToString("F8"));
        // Debug.Log("Parent rotation : " + this.parent.rotation.ToString("F8"));
        // Debug.Log("Child rotation : " + this.child.rotation.ToString("F8"));
    }

    void FixedUpdate()
    {
        float f = flip ? -1f : 1f;
        this.rgbd.MovePosition(this.transform.position + new Vector3(0,0,f) * Time.fixedDeltaTime);
        Debug.Log(this.rgbd.velocity);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enter");
    }
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exit");
    }
}
