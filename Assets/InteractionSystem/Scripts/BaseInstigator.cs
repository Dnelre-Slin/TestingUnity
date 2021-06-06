using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseInstigator : MonoBehaviour
{
    [SerializeField]
    protected string actionMapName = "Player";
    [SerializeField]
    protected Text interactText;

    protected BaseInteractable currentInteractable;

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

    protected void Start()
    {
        if (this.interactText != null)
        {
            this.interactText.enabled = false;
        }
    }

    virtual protected void LookForInteractable()
    {
    }

    public void Instigate()
    {
        if (this.currentInteractable != null)
        {
            this.currentInteractable.Interact();
            this.UpdateText(this.currentInteractable.GetDescription(), true);
        }
    }

    protected void UpdateText(string newText, bool enable)
    {
        if (interactText != null)
        {
            interactText.text = newText;
            interactText.enabled = this.showText ? enable : false;
        }
    }
}
