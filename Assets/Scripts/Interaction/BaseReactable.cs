using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseReactable : MonoBehaviour
{
    [SerializeField]
    protected string description = "Interact";

    virtual public string GetDescription()
    {
        return description;
    }

    virtual public void TriggerReaction()
    {
    }
}
