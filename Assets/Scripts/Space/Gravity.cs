using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    private float initGravityConstant = 100f;
    public static float gravityConstant = 100f;

    [SerializeField]
    private List<GameObject> bodies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Gravity.gravityConstant = this.initGravityConstant;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject body in this.bodies)
        {
            Vector3 force = CalculateTotalForce(body);
            body.GetComponent<Rigidbody>().velocity += force * Time.deltaTime;

            //foreach (GameObject otherBody in this.bodies)
            //{
            //    if (body != otherBody)
            //    {
            //        //Vector3 vec = otherBody.transform.position - body.transform.position;
            //        //float radius = vec.magnitude;
            //        //Vector3 force = this.gravityConstant * (otherBody.GetComponent<Rigidbody>().mass / (radius * radius)) * vec.normalized;
            //        Vector3 force = CalculateForce(body, otherBody);
            //        body.GetComponent<Rigidbody>().velocity += force * Time.deltaTime;
            //    }
            //}
        }
    }

    public Vector3 CalculateTotalForce(GameObject thisBody)
    {
        Vector3 force = Vector3.zero;
        foreach (GameObject otherBody in this.bodies)
        {
            if (thisBody != otherBody)
            {
                force += CalculateForce(thisBody, otherBody);
            }
        }
        return force;
    }

    public Vector3 CalculateTotalForce(Vector3 thisPos, int thisIndex, Vector3[] otherPos, float[] otherMasses)
    {
        if (otherPos.Length != otherMasses.Length)
        {
            throw new System.Exception("otherPos and otherMasses arrays must have the same length");
        }
        Vector3 force = Vector3.zero;
        for(int i = 0; i < otherPos.Length; i++)
        {
            if (i != thisIndex)
            {
                force += CalculateForce(thisPos, otherPos[i], otherMasses[i]);
            }
        }
        return force;
    }

    public static Vector3 CalculateForce(GameObject thisBody, GameObject otherBody)
    {
        return CalculateForce(thisBody.transform.position, otherBody.transform.position, otherBody.GetComponent<Rigidbody>().mass);
    }
    public static Vector3 CalculateForce(Vector3 thisPos, Vector3 otherPos, float otherMass)
    {
        Vector3 vec = otherPos - thisPos;
        float radius = vec.magnitude;
        Vector3 force = Gravity.gravityConstant * (otherMass / (radius * radius)) * vec.normalized;
        return force;
    }

    public List<GameObject> GetBodies()
    {
        return this.bodies;
    }
}
