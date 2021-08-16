using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassthroughDescriptionWorldTextInteractable : PassthroughDescriptionInteractable
{
    [SerializeField]
    protected Text interactableWorldText;

    override public string GetDescription()
    {
        return "";
    }

    virtual protected void UpdateText(string newText, bool enable)
    {
        if (this.interactableWorldText != null)
        {
            this.interactableWorldText.text = newText;
            this.interactableWorldText.enabled = enable;
        }
    }

    override public void Refresh()
    {
        this.UpdateText(base.GetDescription(), true);
    }

    override public void Clear()
    {
        this.UpdateText("", false);
    }
}
