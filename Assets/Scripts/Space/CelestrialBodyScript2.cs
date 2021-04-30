using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestrialBodyScript2 : MonoBehaviour
{
    [SerializeField]
    private Vector3 _initVelocity = new Vector3(10f, 0f, 0f);
    public Vector3 initVelocity
    {
        get { return this._initVelocity; }
        set
        {
            this._initVelocity = value;
        }
    }
    [SerializeField]
    private float _mass = 1f;
    public float mass
    {
        get { return this._mass; }
        set
        {
            this._mass = value;
        }
    }
    public bool drawOrbit = false;

    public Color orbitLineColor = Color.white;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
