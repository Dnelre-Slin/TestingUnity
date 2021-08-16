using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassthroughDescriptionWorldTextDoubleSidedInteractable : PassthroughDescriptionWorldTextInteractable
{
    [SerializeField]
    protected Text interactableWorldTextBack;

    override protected void UpdateText(string newText, bool enable)
    {
        base.UpdateText(newText, enable);

        if (this.interactableWorldTextBack != null)
        {
            this.interactableWorldTextBack.text = newText;
            this.interactableWorldTextBack.enabled = enable;
        }
    }
}
