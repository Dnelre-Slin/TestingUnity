using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] verticies;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //CreateShape();
        CreateTerrain();
        UpdateMesh();
    }

    void CreateShape()
    {
        verticies = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 1),
            new Vector3(0, -1, 0),
            new Vector3(0, -1, 1),
            new Vector3(1, -1, 0),
            new Vector3(1, -1, 1),
        };

        triangles = new int[]
        {
            0, 1, 2, // Top
            1, 3, 2, // Top
            1, 0, 4, // Left
            1, 4, 5, // Left
            2, 3, 6, // Right
            3, 7, 6, // Right
            0, 6, 4, // Front
            0, 2, 6, // Front
            1, 7, 3, // Back
            1, 5, 7, // Back
            4, 6, 5, // Bottom
            5, 6, 7  // Bottom

        };
    }

    void CreateTerrain()
    {
        int x_max = 100;
        int z_max = 100;

        float y_range = 10.0f;
        float height_scale = 1 / (Mathf.PI * 5);

        verticies = new Vector3[x_max * z_max];
        triangles = new int[6 * (x_max - 1) * (z_max - 1)];

        //float yOuter = 0.0f;

        

        for (int z = 0; z < z_max; z++)
        {
            //yOuter += Random.Range(-0.1f, 0.1f);
            //float y = yOuter;
            for (int x = 0; x < x_max; x++)
            {
                //y += Random.Range(-0.1f, 0.1f);
                float p = Mathf.PerlinNoise((float)x * height_scale, (float)z * height_scale);
                Debug.Log(p);
                float y = y_range * p;
                verticies[z * x_max + x] = new Vector3(x, y, z);
            }
        }

        for (int i = 0; i < triangles.Length / 6; i++)
        {
            int tri = i * 6; // The triangle-vertex index

            int v = i + (i / (x_max - 1)); // Find the vertex to calculate from. The division part is to make sure the last column is skiped. (if x_max is 10, v = 10 when i = 9)

            // First triangle
            triangles[tri++] = v;
            triangles[tri++] = v + x_max;
            triangles[tri++] = v + 1;

            // Second triangle
            triangles[tri++] = v + 1;
            triangles[tri++] = v + x_max;
            triangles[tri]   = v + x_max + 1;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verticies;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
