using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassthroughDescriptionInteractable : BaseInteractable
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    override public string GetDescription()
    {
        if (this.baseReactable != null)
        {
            return this.baseReactable.GetDescription();
        }
        return this.description;
    }

}
