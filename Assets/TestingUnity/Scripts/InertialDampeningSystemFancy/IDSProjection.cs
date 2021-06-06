using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDSProjection : MonoBehaviour
{
    public Transform realTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = this.realTransform.localPosition;
        this.transform.localRotation = this.realTransform.localRotation;
        this.transform.localScale = this.realTransform.localScale;
    }
}
