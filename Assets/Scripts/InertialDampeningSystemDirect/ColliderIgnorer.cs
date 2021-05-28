using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderIgnorer : MonoBehaviour
{
    // [SerializeField]
    // private GameObject internalParent = null;
    // [SerializeField]
    // private List<Collider> ignoreColliders = new List<Collider>();
    // Start is called before the first frame update
    void Start()
    {
        // Ignore all collision between internal and exterior shell
        // if (internalParent != null)
        // {
        //     Collider[] internalColliders = internalParent.GetComponentsInChildren<Collider>();
        //     foreach (var exteriorCol in ignoreColliders) // Exterior shell colliders
        //     {
        //         foreach (var interiorCol in internalColliders)
        //         {
        //             Physics.IgnoreCollision(exteriorCol, interiorCol);
        //         }
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
