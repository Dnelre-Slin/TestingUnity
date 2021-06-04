using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// [CustomEditor(typeof(GravityLevelScript))]
[CustomEditor(typeof(OrbitLineEntity))]
public class DrawOrbitalLinesEditor : Editor
{
    void OnEnable()
    {
        SceneView.duringSceneGui += CustomOnSceneGUI;
    }
    void OnDisable()
    {
        SceneView.duringSceneGui -= CustomOnSceneGUI;
    }

    private void CustomOnSceneGUI(SceneView sceneView)
    {
        OrbitLineEntity orbitLineEntity = target as OrbitLineEntity;

        if (orbitLineEntity.gravityScript != null)
        {
            int nr_of_points = orbitLineEntity.gravityScript.GetNrOfStates();
            float timeStep = orbitLineEntity.gravityScript.GetTimeStep();

            foreach (var index in orbitLineEntity.gravityScript.GetDrawIndices())
            {
                Vector3[] points = new Vector3[nr_of_points];
                float t = 0f;

                Handles.color = orbitLineEntity.gravityScript.GetState(t).celestBodies[index].celestrialBody.orbitLineColor;

                for (int i = 0; i < points.Length; i++, t += timeStep)
                {
                    points[i] = orbitLineEntity.gravityScript.GetState(t).celestBodies[index].position;
                }

                Handles.DrawPolyLine(points);
            }
        }
    }

    // private void CustomOnSceneGUI(SceneView sceneView)
    // {
    //     Handles.color = Color.red;
    //     GravityLevelScript gravityLevelScript = target as GravityLevelScript;

    //     int nr_of_points = gravityLevelScript.GetNrOfStates();
    //     float timeStep = gravityLevelScript.GetTimeStep();

    //     foreach (var index in gravityLevelScript.GetDrawIndices())
    //     {
    //         Vector3[] points = new Vector3[nr_of_points];
    //         float t = 0f;

    //         for (int i = 0; i < points.Length; i++, t += timeStep)
    //         {
    //             points[i] = gravityLevelScript.GetState(t).celestBodies[index].position;
    //         }

    //         Handles.DrawPolyLine(points);
    //     }

    //     //Handles.DrawPolyLine(CalculateEarthPath(0.25f, scale));

    //     // foreach (List<Vector3> path in paths)
    //     // {
    //     //     Handles.DrawPolyLine(path.ToArray());
    //     // }
    // }

}
