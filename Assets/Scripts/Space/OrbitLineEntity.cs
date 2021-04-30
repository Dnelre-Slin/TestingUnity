using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitLineEntity : MonoBehaviour
{
    [SerializeField]
    private GravityLevelScript gravityLevelScript = null;

    public GravityLevelScript gravityScript
    {
        get { return this.gravityLevelScript; }
    }

    void OnValidate()
    {
        Debug.Log("Change!");
    }
}
