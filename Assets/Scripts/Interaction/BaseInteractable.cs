using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    [SerializeField]
    protected BaseReactable baseReactable;

    [SerializeField]
    protected string description = "Interact";

    virtual public string GetDescription()
    {
        return this.description;
    }

    virtual public void Interact()
    {
        baseReactable.TriggerReaction();
    }
}
