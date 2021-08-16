using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectLookWorldTextInstigator : DirectLookInstigator
{
    override protected void SetCurrentInteractable(BaseInteractable newInteractable)
    {
        base.SetCurrentInteractable(newInteractable);

        if (this.currentInteractable != null)
        {
            this.interactText = this.currentInteractable.GetText();
        }
        else
        {
            this.UpdateText("", false);
            this.interactText = null;
        }
    }
}
