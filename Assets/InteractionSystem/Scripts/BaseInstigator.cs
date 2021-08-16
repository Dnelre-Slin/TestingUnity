using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInstigator : MonoBehaviour
{
    [SerializeField]
    protected string actionMapName = "Player";

    protected BaseInteractable currentInteractable;


    public void Instigate()
    {
        if (this.currentInteractable != null)
        {
            this.currentInteractable.Interact();
            this.Refresh();
        }
    }

    virtual protected void SetCurrentInteractable(BaseInteractable newInteractable)
    {
        this.currentInteractable = newInteractable;
    }

    virtual public void Refresh()
    {
        if (this.currentInteractable != null)
        {
            this.currentInteractable.Refresh();
        }
    }
    virtual public void Clear()
    {
        if (this.currentInteractable != null)
        {
            this.currentInteractable.Clear();
        }
    }
}
