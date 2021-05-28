﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Controllable))]
public abstract class BaseInstigator : MonoBehaviour
{
    [SerializeField]
    protected string actionMapName = "Player";
    protected Controllable controllable;

    protected BaseInteractable currentInteractable;

    [SerializeField]
    protected Text interactText;

    // Start is called before the first frame update
    protected void Start()
    {
        this.controllable = GetComponent<Controllable>();

        this.controllable.AddActionMap(actionMapName);
        this.controllable.AddAction(actionMapName, "Interact", ActionTypeHandler.ActionType.Performed, OnInteract);

        if (this.interactText != null)
        {
            this.interactText.enabled = false;
        }
    }

    virtual protected void LookForInteractable()
    {
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (this.currentInteractable != null)
        {
            this.currentInteractable.Interact();
            // interactText.text = this.currentInteractable.GetDescription();
            this.UpdateText(this.currentInteractable.GetDescription(), true);
        }
    }

    protected void UpdateText(string newText, bool enable)
    {
        if (interactText != null)
        {
            interactText.text = newText;
            interactText.enabled = this.controllable.isControlled ? enable : false;
        }
    }
}