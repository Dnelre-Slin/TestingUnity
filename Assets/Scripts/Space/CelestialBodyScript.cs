using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CelestialBodyScript : MonoBehaviour
{
    private Rigidbody rgbd;

    [SerializeField]
    private Rigidbody sun = null;

    [SerializeField]
    private float initSpeed = 100f;

    [SerializeField]
    private Vector3 initDirection = new Vector3(1f, 0f, 0f);

    //private Vector3[] trail = new Vector3[100];

    //private Vector3[] path = new Vector3[40];

    // Start is called before the first frame update
    void Start()
    {
        rgbd = this.GetComponent<Rigidbody>();

        //for (int i = 0; i < trail.Length; i++)
        //{
        //    trail[i] = this.transform.position;
        //}

        this.rgbd.velocity = initDirection.normalized * initSpeed;

        //CalculatePath();
        //StartCoroutine(UpdatePath());
        //StartCoroutine(UpdateTrail());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    //public Vector3[] GetTrail()
    //{
    //    return this.trail;
    //}

    //public Vector3[] GetPath()
    //{
    //    return this.path;
    //}

    public Rigidbody GetSunRgbd()
    {
        return sun;
    }

    public Vector3 GetInitVelocity()
    {
        return this.initSpeed * this.initDirection;
    }

    public Vector3 GetVelocity()
    {
        if (EditorApplication.isPlaying)
        {
            return this.rgbd.velocity;
        }
        else
        {
            return this.initSpeed * this.initDirection;
        }
    }

    //private void CalculatePath()
    //{
    //    Vector3 sunPos = sun.transform.position;
    //    float sunMass = sun.mass;
    //    Vector3 position = this.transform.position;
    //    Vector3 velocity = this.rgbd.velocity;

    //    float timeStep = 1f;

    //    this.path[0] = position;

    //    for (int i = 1; i < this.path.Length; i++)
    //    {
    //        Vector3 force = Gravity.CalculateForce(position, sunPos, sunMass);
    //        velocity += force * timeStep;
    //        position += velocity * timeStep;
    //        this.path[i] = position;
    //    }
    //}

    //IEnumerator UpdatePath()
    //{
    //    while (true)
    //    {
    //        CalculatePath();
    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    //IEnumerator UpdateTrail()
    //{
    //    while (true)
    //    {
    //        for (int i = 0; i < trail.Length - 1; i++)
    //        {
    //            trail[i] = trail[i + 1];
    //        }
    //        trail[trail.Length - 1] = this.transform.position;
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}
}
