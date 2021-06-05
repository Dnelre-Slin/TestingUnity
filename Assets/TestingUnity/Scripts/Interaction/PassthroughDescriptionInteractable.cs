using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassthroughDescriptionInteractable : BaseInteractable
{
    override public string GetDescription()
    {
        if (this.baseReactable != null)
        {
            return this.baseReactable.GetDescription();
        }
        return this.description;
    }

}
