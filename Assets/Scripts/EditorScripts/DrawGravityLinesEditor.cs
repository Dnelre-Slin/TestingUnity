using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Gravity))]
public class DrawGravityLinesEditor : Editor
{
    static float scale = 0.6f;

    private Vector3[] path; // = new Vector3[30];

    void OnEnable()
    {
        // SceneView.onSceneGUIDelegate += (SceneView.OnSceneFunc)Delegate.Combine(SceneView.onSceneGUIDelegate, new SceneView.OnSceneFunc(CustomOnSceneGUI));
        SceneView.duringSceneGui += CustomOnSceneGUI;
    }

    private void CustomOnSceneGUI(SceneView sceneView)
    {
        //scale = EditorGUILayout.Slider(scale, 0, 10);

        //Gravity gravityScript = target as Gravity;
        //EarthScript earth = target as EarthScript;
        //Vector3 earthPos = earth.GetPosition();
        //positions = new Vector3[3];
        //positions[0] = earthPos + new Vector3(0, 0, 0);
        //positions[1] = earthPos + new Vector3(50, 0, 0);
        //positions[2] = earthPos + new Vector3(50, 0, 50);

        //Vector3[] earthPath;

        //Debug.Log(earthPath);

        //if (EditorApplication.isPlaying)
        //{
        //    //earthPath = earth.GetPath();
        //    earthPath = new Vector3[0];
        //    //
        //}
        //else
        //{
        //    path = CalculateEarthPath(30, 1f);
        //    earthPath = this.path;
        //}

        List<Vector3>[] paths = CalculateFuturePaths(0.25f, scale);

        Handles.color = Color.red;
        //Handles.DrawPolyLine(CalculateEarthPath(0.25f, scale));

        foreach (List<Vector3> path in paths)
        {
            Handles.DrawPolyLine(path.ToArray());
        }

        //Handles.color = Color.green;
        //Handles.DrawPolyLine(CalculateEarthPath(3000, 0.01f));

        //Vector3[] earthTrail = earth.GetTrail();

        //Handles.color = Color.red;
        //Handles.DrawPolyLine(earthTrail);
    }

        private List<Vector3>[] CalculateFuturePaths(float timeStep, float scale = 1f, int maxPathSize = 2000)
    {
        Gravity gravityScript = target as Gravity;
        List<GameObject> bodies = gravityScript.GetBodies();

        List<Vector3>[] paths = new List<Vector3>[bodies.Count];

        Vector3[] startPositions = new Vector3[bodies.Count];
        Vector3[] positions = new Vector3[bodies.Count];
        Vector3[] velocities = new Vector3[bodies.Count];
        float[] masses = new float[bodies.Count];

        float[] posSteps = new float[bodies.Count];
        bool[] done = new bool[bodies.Count];

        int isRunning = (int)Mathf.Pow(2, bodies.Count) - 1;

        for(int i = 0; i < bodies.Count; i++)
        {
            startPositions[i] = positions[i] = bodies[i].transform.position;
            velocities[i] = bodies[i].GetComponent<CelestialBodyScript>().GetVelocity();
            masses[i] = bodies[i].GetComponent<Rigidbody>().mass;

            posSteps[i] = velocities[i].magnitude * timeStep;

            done[i] = velocities[i].magnitude < 0.1f;

            paths[i] = new List<Vector3>(maxPathSize);
            paths[i].Add(positions[i]);
        }

        for (int i = 1; i < maxPathSize; i++)
        {
            for (int j = 0; j < bodies.Count; j++)
            {
                Vector3 force = gravityScript.CalculateTotalForce(positions[j], j, positions, masses);
                velocities[j] += force * (1 - Mathf.Pow((scale / i), 2f)) * timeStep;
                positions[j] += velocities[j] * timeStep;
                if (!done[j])
                {
                    paths[j].Add(positions[j]);
                }
                //if (Vector3.Distance(startPos, position) < posStep / 2)
                if (Vector3.Distance(startPositions[j], positions[j]) < posSteps[j] / 2f)
                {
                    done[j] = true;
                    isRunning &= ~(int)Mathf.Pow(2, j);
                }
            }
            if (isRunning == 0)
            {
                break;
            }
        }

        return paths;
    }

        private Vector3[] CalculateEarthPath(float timeStep, float scale = 1f, int maxPathSize = 10000)
    {
        //Vector3[] path = new Vector3[pathSize];
        List<Vector3> path = new List<Vector3>(maxPathSize);

        CelestialBodyScript earthScript = target as CelestialBodyScript;
        GameObject earth = earthScript.gameObject;
        Rigidbody sun = earthScript.GetSunRgbd();

        Vector3 sunPos = sun.transform.position;
        float sunMass = sun.mass;
        Vector3 position = earth.transform.position;
        Vector3 startPos = position;
        //Vector3 velocity;
        //if (EditorApplication.isPlaying)
        //{
        //    velocity = earth.GetComponent<Rigidbody>().velocity;
        //}
        //else
        //{
        //    velocity = earthScript.GetInitVelocity();
        //}
        //Vector3 velocity = earth.GetComponent<Rigidbody>().velocity;
        Vector3 velocity = earthScript.GetVelocity();

        float posStep = velocity.magnitude * timeStep;

        path.Add(position);
        //path[0] = position;

        for (int i = 1; i < maxPathSize; i++)
        {
            Vector3 force = Gravity.CalculateForce(position, sunPos, sunMass);
            velocity += force * (1 - Mathf.Pow((scale / i),2f)) * timeStep;
            position += velocity * timeStep;
            path.Add(position);
            //path[i] = position;
            if (Vector3.Distance(startPos, position) < posStep / 2)
            {
                break;
            }
        }

        return path.ToArray();
        //return path;
    }
}
