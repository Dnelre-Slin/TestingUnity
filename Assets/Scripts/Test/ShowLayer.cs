using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLayer : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.layer = layerMask.value;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Layermask: " + this.gameObject.layer);
    }
}
