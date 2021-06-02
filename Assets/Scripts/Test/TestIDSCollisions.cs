using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIDSCollisions : MonoBehaviour
{
    [SerializeField]
    List<Collider> ignoreColliders = new List<Collider>();
    [SerializeField]
    List<TestImpulseAdder> impulseAdders = new List<TestImpulseAdder>();
    [SerializeField]
    bool execute = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var col1 in this.ignoreColliders)
        {
            foreach (var col2 in this.ignoreColliders)
            {
                if (col1 != col2)
                {
                    Physics.IgnoreCollision(col1, col2);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.execute)
        {
            foreach (var impulser in impulseAdders)
            {
                impulser.Execute();
            }
            this.execute = false;
        }
    }
}
