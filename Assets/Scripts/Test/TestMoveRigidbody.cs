using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestMoveRigidbody : MonoBehaviour
{
    [SerializeField]
    private Vector3 vel = Vector3.zero;
    private Rigidbody rgbd = null;
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rgbd.velocity = vel;
    }
}
