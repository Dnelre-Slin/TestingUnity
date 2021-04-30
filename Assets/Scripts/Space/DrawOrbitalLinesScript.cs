using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GravityLevelScript))]
public class DrawOrbitalLinesScript : MonoBehaviour
{
    private GravityLevelScript gravityLevelScript;

    [SerializeField]
    private float sleepTime = 0.5f;
    private float lastTime = 0.0f;
    // Start is called before the first frame update
    void OnValidate()
    {
        this.gravityLevelScript = GetComponent<GravityLevelScript>();
    }
    void Start()
    {
        this.gravityLevelScript = GetComponent<GravityLevelScript>();
    }

    void OnDrawGizmos()
    {
        // Gizmos.color = Color.blue;
        // Gizmos.color = Color.blue;
        // Debug.Log("Superman can fly!");
        if (!Application.IsPlaying(this) && Time.time + this.sleepTime > this.lastTime)
        {
            // Debug.Log("Aquaman can swim!");
            this.gravityLevelScript.FirstTimeCalculateOrbits();
            this.lastTime = Time.time;
        }
        // this.gravityLevelScript.FirstTimeCalculateOrbits();

        int nr_of_points = this.gravityLevelScript.GetNrOfStates();
        float timeStep = this.gravityLevelScript.GetTimeStep();

        foreach (var index in gravityLevelScript.GetDrawIndices())
        {
            Vector3[] points = new Vector3[nr_of_points];
            float t = 0f;

            Gizmos.color = this.gravityLevelScript.GetState(t).celestBodies[index].celestrialBody.orbitLineColor;

            for (int i = 0; i < points.Length; i++, t += timeStep)
            {
                points[i] = this.gravityLevelScript.GetState(t).celestBodies[index].position;
            }

            for (int i = 1; i < points.Length; i++)
            {
                Gizmos.DrawLine(points[i-1], points[i]);
            }
        }
    }

    // void DrawMultiPointLine(Vector3[] points)
    // {
    //     for (int i = 1; i < points.Length; i++)
    //     {
    //        Gizmos.DrawLine(points[i-1], points[i]);
    //     }
    // }
}
