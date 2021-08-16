using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectLookUiTextInstigator : DirectLookInstigator, IInstigatorWithUiText
{
    [SerializeField]
    protected Text interactText;

    protected bool _showText = true;
    public bool showText {
        get { return this._showText; }
        set {
            this._showText = value;
            if (interactText != null)
            {
                interactText.enabled = interactText.enabled ? this._showText : false;
            }
        }
    }

    new void Start()
    {
        base.Start();
        if (this.interactText != null)
        {
            this.interactText.enabled = false;
        }
    }

    protected void UpdateText(string newText, bool enable)
    {
        if (this.interactText != null)
        {
            this.interactText.text = newText;
            this.interactText.enabled = this.showText ? enable : false;
        }
    }

    override public void Refresh()
    {
        base.Refresh();
        if (this.currentInteractable != null)
        {
            this.UpdateText(this.currentInteractable.GetDescription(), true);
        }
    }

    override public void Clear()
    {
        base.Clear();
        this.UpdateText("", false);
    }

    public void SetShowText(bool newShowText)
    {
        this.showText = newShowText;
    }

    public bool GetShowText()
    {
        return this.showText;
    }
}
