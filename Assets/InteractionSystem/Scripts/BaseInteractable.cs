using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseInteractable : MonoBehaviour
{
    [SerializeField]
    protected BaseReactable baseReactable;

    [SerializeField]
    protected string description = "Interact";

    [SerializeField]
    protected Text interactableWorldText;

    virtual public string GetDescription()
    {
        return this.description;
    }

    virtual public void Interact()
    {
        if (this.baseReactable != null)
        {
            this.baseReactable.TriggerReaction();
        }
    }

    virtual public Text GetText()
    {
        return this.interactableWorldText;
    }
}
